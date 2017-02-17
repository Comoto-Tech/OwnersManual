using System;
using NUnit.Framework;
using OwnersManual.Features.Hashing;
using OwnersManual.Integrations.Confluence;
using OwnersManual.Integrations.Confluence.Api;

namespace OwnersManual.IntegrationTests
{
    public class BeatUpTheTestPage
    {
        RestfulConfluenceApi _updater;
        int pageId = 115818626;

        [SetUp]
        public void Y()
        {
            _updater = new RestfulConfluenceApi(ConfluenceConfig.FromConfig(), new SHA1Hasher());
        }
        [Test]
        public void X()
        {
            var oldPage = _updater.Get(pageId);

            var newContent = $@"# Hi

All the things

[Link](http://google.com)

> Quotes

---

**BOB**";
            var result = _updater.Put(oldPage, newContent);
            Console.WriteLine(result);
        }
    }
}
