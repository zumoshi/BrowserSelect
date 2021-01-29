using BrowserSelect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPatternGeneration()
        {
            var tests = (new[]
            {
                new[]{"URL", "http://google.com", "*.google.com"},
                new[]{"URL", "http://www.google.com", "*.google.com"},
                new[]{"URL", "http://google.au", "*.google.au"},
                new[]{"URL", "http://something.com.au", "*.something.com.au"},
                new[]{"URL", "http://www.info.com", "*.info.com"},
                new[]{"URL", "http://info.com", "*.info.com"},
                new[]{"URL", "http://something.info.au", "amg"},
                new[]{"URL", "http://linux.conf.au", "amg"},
                new[]{"URL", "http://news.vic.au", "amg"},
                new[]{"URL", "http://www.news.vic.au", "*.news.vic.au"},
                new[]{"URL", "http://www.something.info.au", "*.something.info.au"},
                new[]{"URL", "http://something.id.au", "*.something.id.au"},
                new[]{"URL", "http://localhost", "*.localhost"}
            });
            foreach (var test in tests)
            {
                var rule = BrowserSelectView.generateRule(test[0]);
                var check = "bug";
                switch (rule.mode)
                {
                    case 3:
                        check = "amg"; break;
                    case 2:
                        check = rule.second_rule; break;
                    case 1:
                        check = rule.tld_rule; break;
                }
                Assert.AreEqual(check, test[1]);
            }
        }
    }
}
