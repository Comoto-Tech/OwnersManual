using System;

namespace OwnersManual
{
    public class ConfluenceConfiguration
    {
        public Uri Endpoint { get; set; }
        public string SpaceKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ConfluenceConfiguration FromConfig()
        {
            return new ConfluenceConfiguration
            {
                Endpoint = new Uri("https://magdevelopment.atlassian.net/wiki/rest/api"),
                Username = "OwnersManual",
                Password = "OwnersManual",
                SpaceKey = "SYS"
            };
        }
    }
}
