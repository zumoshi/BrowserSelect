using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserSelect;

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
                new[]{"http://google.com", "*.google.com"},
                new[]{"http://www.google.com", "*.google.com"},
                new[]{"http://google.au", "*.google.au"},
                new[]{"http://something.com.au", "*.something.com.au"},
                new[]{"http://www.info.com", "*.info.com"},
                new[]{"http://info.com", "*.info.com"},
                new[]{"http://something.info.au", "amg"},
                new[]{"http://linux.conf.au", "amg"},
                new[]{"http://news.vic.au", "amg"},
                new[]{"http://www.news.vic.au", "*.news.vic.au"},
                new[]{"http://www.something.info.au", "*.something.info.au"},
                new[]{"http://something.id.au", "*.something.id.au"}
            });
            foreach (var test in tests)
            {
                var rule = Form1.generate_rule(test[0]);
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
