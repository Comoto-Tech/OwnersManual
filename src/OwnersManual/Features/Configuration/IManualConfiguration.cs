using OwnersManual.Integrations.Confluence;

namespace OwnersManual.Features.Configuration
{
    public interface IManualConfiguration
    {
        void WriteToConfluence(ConfluenceConfig cfg);
        void WriteToConsole();
    }
}