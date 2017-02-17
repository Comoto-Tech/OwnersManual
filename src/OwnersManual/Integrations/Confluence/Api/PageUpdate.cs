using System.Collections.Generic;

namespace OwnersManual.Integrations.Confluence.Api
{
    public class PageUpdate
    {
        public PageVersion version { get; set; }
        public List<PageAncestors> ancestors { get; set; }
        public string type { get; set; }
        public PageBody body { get; set; }

        public string title { get; set; }
    }
}