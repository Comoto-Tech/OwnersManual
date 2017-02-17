using NUnit.Framework;
using OwnersManual.Features.Configuration;

namespace OwnersManual.UnitTests
{
    public class DesignNotes
    {
        [Test]
        public void Smoke()
        {
            var m = Manual.Build(cfg =>
            {
                //cfg.WriteToConfluence(ConfluenceConfig.FromConfig());
                cfg.WriteToConsole();
            });

            m.Document("Title", "Content");
            m.Document("Title2", "Content");

            m.Flush();
        }
    }
}
