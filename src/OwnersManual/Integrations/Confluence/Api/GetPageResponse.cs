namespace OwnersManual.Integrations.Confluence.Api
{
    public class GetPageResponse
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public GetPageBody body { get; set; }
        public GetPageVersion version { get; set; }
        public string Hash { get; set; }
    }
}
