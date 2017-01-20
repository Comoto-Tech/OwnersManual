using OwnersManual.Api;

namespace OwnersManual
{
    public interface IPageUpdater
    {
        GetPageResponse Get(int pageId);
        PutResult Put(GetPageResponse oldPage, string content);
    }
}
