using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CommonMark;
using OwnersManual.Api;
using RestSharp;
using RestSharp.Authenticators;

namespace OwnersManual
{
    public class RestfulPageUpdater : IPageUpdater
    {
        readonly IRestClient _restClient;
        readonly ConfluenceConfiguration _cfg;

        public RestfulPageUpdater(ConfluenceConfiguration cfg)
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

            resp.Data.Hash = Hash(resp.Data.body.view.value);

            return resp.Data;
        }

        public string Hash(string clearText)
        {
            using (var s = new SHA1Managed())
            {
                byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
                byte[] cipherBytes = s.ComputeHash(clearBytes);
                return Convert.ToBase64String(cipherBytes);
            }
        }

        public PutResult Put( GetPageResponse oldPage, string content)
        {
            var convertedContent = CommonMarkConverter.Convert(content, CommonMarkSettings.Default);
            var hash = Hash(convertedContent);

            if (hash == oldPage.Hash)
            {
                Debug.WriteLine("HASHes match, ignoring.");
                return PutResult.Duplicate;
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
                return PutResult.Ok;
            }

            return PutResult.Unknown;
        }
    }
}
