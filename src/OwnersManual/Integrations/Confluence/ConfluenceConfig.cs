using System;
using System.Configuration;

namespace OwnersManual.Integrations.Confluence
{
    public class ConfluenceConfig
    {
        public int PageId { get; set; }
        public Uri Endpoint { get; set; }
        public string SpaceKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ConfluenceConfig FromConfig()
        {
            return new ConfluenceConfig
            {
                Endpoint = new Uri(ConfigurationManager.AppSettings["ConfluenceConfig.Endpoint"]),
                Username = ConfigurationManager.AppSettings["ConfluenceConfig.Username"],
                Password = ConfigurationManager.AppSettings["ConfluenceConfig.Password"],
                SpaceKey = ConfigurationManager.AppSettings["ConfluenceConfig.SpaceKey"],
                PageId = int.Parse(ConfigurationManager.AppSettings["ConfluenceConfig.PageId"])
            };
        }
    }
}
