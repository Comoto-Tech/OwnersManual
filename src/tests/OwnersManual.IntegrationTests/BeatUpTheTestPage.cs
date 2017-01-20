using System;
using NUnit.Framework;

namespace OwnersManual.IntegrationTests
{
    public class BeatUpTheTestPage
    {
        IPageUpdater _updater;
        int pageId = 115818626;

        [SetUp]
        public void Y()
        {
            _updater = new RestfulPageUpdater(ConfluenceConfiguration.FromConfig());
        }
        [Test]
        public void X()
        {
            var oldPage = _updater.Get(pageId);

            var newContent = $@"# {Guid.NewGuid()}

All the things

[Link](http://google.com)

> Quotes

---

**BOB**";
            _updater.Put(oldPage, newContent);
        }
    }
}
