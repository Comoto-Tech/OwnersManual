using System.Collections.Generic;
using OwnersManual.Features.Hashing;
using OwnersManual.Features.Writers;
using OwnersManual.Integrations.Confluence;
using OwnersManual.Integrations.Console;

namespace OwnersManual.Features.Configuration
{
    public class ManualConfiguration : IManualConfiguration
    {
        public IList<IDocumentationWriter> Updaters { get; }= new List<IDocumentationWriter>();

        public void WriteToConfluence(ConfluenceConfig cfg)
        {
            Updaters.Add(new ConfluenceDocumentationWriter(cfg, new SHA1Hasher()));
        }

        public void WriteToConsole()
        {
            Updaters.Add(new ConsoleWriter());
        }
    }
}