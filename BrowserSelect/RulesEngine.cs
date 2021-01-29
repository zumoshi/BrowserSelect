using System;
using System.Linq;

using BrowserSelect.Properties;

namespace BrowserSelect
{
    class RulesEngine
    {
        // struct used to store patterns created for Create Rule button
        public struct AmbiguousRule
        {
            public string firstRule;
            public string secondRule;
            public int mode;
        }

        //-------------------------------------------------------------------------------------------------------------
        public void AddRule(string type, string pattern, BrowserModel browser)
        //-------------------------------------------------------------------------------------------------------------
        {
            SaveRule(type, pattern, browser);

            UrlProcessor processor = new UrlProcessor();
            processor.OpenUrl(browser);
        }

        //-------------------------------------------------------------------------------------------------------------
        private void SaveRule(string type, string pattern, BrowserModel browser)
        //-------------------------------------------------------------------------------------------------------------
        {
            // save a rule and save app settings
            Settings.Default.Rules.Add((new RuleModel()
            {
                Type = type,
                Pattern = pattern,
                Browser = browser.name
            }).ToString());
            Settings.Default.Save();
        }

        //-------------------------------------------------------------------------------------------------------------
        public AmbiguousRule GenerateRule(string url)
        //-------------------------------------------------------------------------------------------------------------
        {
            /*
            to solve issue #13
            there are a lot of second level domains, e.g. domain.info.au, domain.vic.au, ...
            so we check these rules:
            if url has only two parts (e.g. x.tld or www.x.tld) choose *.x.tld
            else if url has 3 parts or more(e.g. y.x.tld) and y!=www:
                check the following rules: (x = second part after tld)
                    1.(x is part of domain)
                        if len(x) > 4: assume that x is not part of extension, and choose  *.x.tld
                    2.(x is part of extension)
                        if len(x) <=2 (e.g. y.id.au) than choose *.y.x.tld
                        if x is in exceptions (com,net,org,edu,gov,asn.sch) choose *.y.x.tld
                            because many TLD's have second level domains on these, e.g. chap.sch.ir
                        if count(parts)==4 and first part is www: e.g. www.news.com.au, choose *.y.x.tld
                if none of the rules apply, the case is ambiguous, display both options in a context menu.
                    e.g. sealake.vic.au or something.fun.ir
            else if url has only one part (#27):
                add *.x
            */

            // needed variables
            var domain = new Uri(url).Host;
            var parts = domain.Split('.');
            var count = parts.Length;
            var firstLevel = parts.Last();
            var secondLevel = "";
            var thirdLevel = "";
            try
            {
                secondLevel = parts[count - 2]; //second-level
                thirdLevel = parts[count - 3]; //third-level
            }
            catch (IndexOutOfRangeException) { } // in case domain did not have 3 parts.. (e.g. localhost, google.com)

            // creating the patterns
            var ruleFirstLevel = String.Format("*.{0}.{1}", secondLevel, firstLevel);
            var ruleSecondLevel = String.Format("*.{0}.{1}.{2}", thirdLevel, secondLevel, firstLevel);
            var mode = 0; // 0 = error, 1=use rule_tld (*.x.tld), 2=use rule_second (*.y.x.tld), 3=ambiguous

            // this conditions are based on the long comment above
            if (count == 2 || (count == 3 && thirdLevel == "www"))
                mode = 1;
            else if (count >= 3)
            {
                if (secondLevel.Length > 4)
                    mode = 1;
                else if (
                    (secondLevel.Length <= 2) ||
                    ((new[] { "com", "net", "org", "edu", "gov", "us", "biz" }).Contains(secondLevel)) ||
                    (count == 4 && parts[0] == "www")
                    )
                    mode = 2;
                else
                    mode = 3;
            }
            else if (count == 1)
            {
                mode = 1;
                ruleFirstLevel = "*." + firstLevel;
            }

            return new AmbiguousRule()
            {
                firstRule = ruleFirstLevel,
                secondRule = ruleSecondLevel,
                mode = mode
            };
        }
    }
}
