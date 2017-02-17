using System.Diagnostics;
using System.Net;
using CommonMark;
using OwnersManual.Features.Hashing;
using OwnersManual.Features.Writers;
using RestSharp;
using RestSharp.Authenticators;

namespace OwnersManual.Integrations.Confluence.Api
{
    public class RestfulConfluenceApi
    {
        readonly IRestClient _restClient;
        readonly ConfluenceConfig _cfg;
        readonly IHasher _hasher;

        public RestfulConfluenceApi(ConfluenceConfig cfg, IHasher hasher)
        {
            _cfg = cfg;
            _hasher = hasher;
            _restClient = new RestClient(cfg.Endpoint);
            _restClient.Authenticator = new HttpBasicAuthenticator(cfg.Username, cfg.Password);
        }

        public GetPageResponse Get(int pageId)
        {
            var req = new RestRequest("/content/{pageId}", Method.GET);
            req.AddUrlSegment("pageId", pageId.ToString());
            req.AddQueryParameter("space", _cfg.SpaceKey);

            req.AddQueryParameter("expand", "body.view,version");


            var resp = _restClient.Execute<GetPageResponse>(req);

            resp.Data.Hash = _hasher.Hash(resp.Data.body.view.value);

            return resp.Data;
        }

        public ContentResult Put( GetPageResponse oldPage, string content)
        {
            var convertedContent = CommonMarkConverter.Convert(content, CommonMarkSettings.Default);
            var hash = _hasher.Hash(convertedContent);

            //TODO: Figure out how to detect if this has changed or not. Hashing the confluence content is not the correct approach.
            if (hash == oldPage.Hash)
            {
                Debug.WriteLine("HASHes match, ignoring.");
                return ContentResult.Duplicate;
            }

            var req = new RestRequest("/content/{pageId}", Method.PUT);
            req.AddUrlSegment("pageId", oldPage.id);

            var pu = new PageUpdate
            {
                version = new PageVersion
                {
                    number = oldPage.version.number + 1
                },
                title = oldPage.title,
                type = oldPage.type,
                body = new PageBody
                {
                    storage = new PageStorage
                    {
                        value = convertedContent,
                        representation = "storage"
                    }
                }
            };

            req.AddJsonBody(pu);
            var resp = _restClient.Execute(req);

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                return ContentResult.Ok;
            }

            return ContentResult.Unknown;
        }
    }
}
