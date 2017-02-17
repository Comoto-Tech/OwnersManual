using System;
using System.Collections.Generic;
using System.Diagnostics;
using OwnersManual.Features.Writers;

namespace OwnersManual.Features.Configuration
{
    public class ManualInstance
    {
        readonly IList<DocumentEntry> _entries = new List<DocumentEntry>();
        readonly IList<IDocumentationWriter> _updaters;

        public ManualInstance(IList<IDocumentationWriter> updaters)
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
                    var x = pageUpdater.Write(_entries);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}