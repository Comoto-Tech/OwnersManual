using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CommonMark;
using OwnersManual.Features.Configuration;
using OwnersManual.Features.Hashing;
using OwnersManual.Features.Writers;
using OwnersManual.Integrations.Confluence.Api;

namespace OwnersManual.Integrations.Confluence
{
    public class ConfluenceDocumentationWriter : IDocumentationWriter
    {
        readonly RestfulConfluenceApi _api;
        readonly ConfluenceConfig _cfg;
        readonly IHasher _hasher;

        public ConfluenceDocumentationWriter(ConfluenceConfig cfg, IHasher hasher)
        {
            _cfg = cfg;
            _api = new RestfulConfluenceApi(cfg);
            _hasher = hasher;
        }

        public UpdateResult Write(IList<DocumentEntry> entries)
        {
            var oldPage = _api.Get(_cfg.PageId);
            var oldHash = _hasher.Hash(oldPage.body.view.value);

            var sb = new StringBuilder();
            foreach (var documentEntry in entries)
            {
                sb.AppendLine(documentEntry.Title);
                sb.AppendLine(documentEntry.Content);
            }

            var convertedContent = CommonMarkConverter.Convert(sb.ToString(), CommonMarkSettings.Default);
            var hash = _hasher.Hash(convertedContent);

            //TODO: Figure out how to detect if this has changed or not. Hashing the confluence content is not the correct approach.
            //TODO: Maybe just look for a key/value pair you can REGEX out that is in the footer
            if (hash == oldHash)
            {
                Debug.WriteLine("HASHes match, ignoring.");
                return new UpdateResult(ContentResult.Duplicate);
            }

            _api.Put(oldPage, sb.ToString());

            return new UpdateResult(ContentResult.Ok);
        }
    }
}
