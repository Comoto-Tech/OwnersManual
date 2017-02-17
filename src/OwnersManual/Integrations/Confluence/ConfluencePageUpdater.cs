using System.Collections.Generic;
using System.Text;
using OwnersManual.Features.Configuration;
using OwnersManual.Features.Hashing;
using OwnersManual.Features.Writers;
using OwnersManual.Integrations.Confluence.Api;

namespace OwnersManual.Integrations.Confluence
{
    public class ConfluencePageUpdater : IPageUpdater
    {
        readonly RestfulConfluenceApi _api;
        readonly ConfluenceConfig _cfg;

        public ConfluencePageUpdater(ConfluenceConfig cfg)
        {
            _cfg = cfg;
            _api = new RestfulConfluenceApi(cfg, new SHA1Hasher());
        }

        public UpdateResult Update(IList<DocumentEntry> entries)
        {
            var old = _api.Get(_cfg.PageId);

            var sb = new StringBuilder();
            foreach (var documentEntry in entries)
            {
                sb.AppendLine(documentEntry.Title);
                sb.AppendLine(documentEntry.Content);
            }

            _api.Put(old, sb.ToString());

            return new UpdateResult();
        }
    }
}
