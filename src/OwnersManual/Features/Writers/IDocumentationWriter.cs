using System.Collections.Generic;
using OwnersManual.Features.Configuration;

namespace OwnersManual.Features.Writers
{
    public interface IDocumentationWriter
    {
        UpdateResult Write(IList<DocumentEntry> entries);
    }
}
