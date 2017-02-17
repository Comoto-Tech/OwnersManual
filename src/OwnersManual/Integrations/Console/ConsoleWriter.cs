using System.Collections.Generic;
using OwnersManual.Features.Configuration;
using OwnersManual.Features.Writers;

namespace OwnersManual.Integrations.Console
{
    public class ConsoleWriter : IPageUpdater
    {
        public UpdateResult Update(IList<DocumentEntry> entries)
        {
            foreach (var documentEntry in entries)
            {
                System.Console.WriteLine(documentEntry.Title);
                System.Console.WriteLine(new string('-', documentEntry.Title.Length));
                System.Console.WriteLine(documentEntry.Content);
                System.Console.WriteLine();
            }

            return new UpdateResult();
        }
    }
}
