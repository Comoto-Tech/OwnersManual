using System.Net;
using OwnersManual.Features.Writers;
using RestSharp;
using RestSharp.Authenticators;

namespace OwnersManual.Integrations.Confluence.Api
{
    public class RestfulConfluenceApi
    {
        readonly IRestClient _restClient;
        readonly ConfluenceConfig _cfg;

        public RestfulConfluenceApi(ConfluenceConfig cfg)
        {
            _cfg = cfg;
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

            return resp.Data;
        }

        public ContentResult Put( GetPageResponse oldPage, string content)
        {
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
                        value = content,
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
