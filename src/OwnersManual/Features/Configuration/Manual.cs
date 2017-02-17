using System;
using System.Collections.Generic;
using System.Diagnostics;
using OwnersManual.Features.Writers;
using OwnersManual.Integrations.Confluence;
using OwnersManual.Integrations.Console;

namespace OwnersManual.Features.Configuration
{
    public static class Manual
    {
        public static ManualInstance Build(Action<IManualConfiguration> action)
        {
            var cfg = new ManualConfiguration();

            action(cfg);

            return new ManualInstance(cfg.Updaters);
        }
    }

    public class ManualConfiguration : IManualConfiguration
    {
        public IList<IPageUpdater> Updaters { get; }= new List<IPageUpdater>();

        public void WriteToConfluence(ConfluenceConfig cfg)
        {
            Updaters.Add(new ConfluencePageUpdater(cfg));
        }

        public void WriteToConsole()
        {
            Updaters.Add(new ConsoleWriter());
        }
    }

    public interface IManualConfiguration
    {
        void WriteToConfluence(ConfluenceConfig cfg);
        void WriteToConsole();
    }

    [DebuggerDisplay("{" + nameof(Title) + "}")]
    public class DocumentEntry
    {
        public DocumentEntry(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }
    }

    public class ManualInstance
    {
        readonly IList<DocumentEntry> _entries = new List<DocumentEntry>();
        readonly IList<IPageUpdater> _updaters;

        public ManualInstance(IList<IPageUpdater> updaters)
        {
            _updaters = updaters;
        }

        public void Document(string title, string content)
        {
            _entries.Add(new DocumentEntry(title, content));
        }

        public void Flush()
        {
            foreach (var pageUpdater in _updaters)
            {
                try
                {
                    var x = pageUpdater.Update(_entries);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
