using System.Collections.Generic;
using OwnersManual.Features.Configuration;

namespace OwnersManual.Features.Writers
{
    public interface IPageUpdater
    {
        UpdateResult Update(IList<DocumentEntry> entries);
    }
}
