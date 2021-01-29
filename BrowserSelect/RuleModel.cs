using System;

namespace BrowserSelect
{
    //=============================================================================================================
    class RuleModel : IComparable
    //=============================================================================================================
    {
        public string Type {get; set;}
        public string Pattern {get; set;}
        public string Browser {get; set;}

        //-------------------------------------------------------------------------------------------------------------
        public static implicit operator RuleModel(System.String s)
        //-------------------------------------------------------------------------------------------------------------
        {
            var ss = s.Split(new[] { "[#!][$~][?_]" }, StringSplitOptions.None);
            return new RuleModel()
            {
                Type = ss[0] != null ? ss[0] : "URL",
                Pattern = ss[1],
                Browser = ss[2]
            };
        }

        //-------------------------------------------------------------------------------------------------------------
        public override string ToString()
        //-------------------------------------------------------------------------------------------------------------
        {
            return Type + "[#!][$~][?_]" + Pattern + "[#!][$~][?_]" + Browser;
        }

        //-------------------------------------------------------------------------------------------------------------
        public string errorMessage()
        //-------------------------------------------------------------------------------------------------------------
        {
            if (!string.IsNullOrEmpty(Pattern))
                return "One of your rules has an empty Pattern. please refer to Help for more information.";
            else if (string.IsNullOrEmpty(Type))
                return string.Format("You forgot to select a Type for '{0}' rule.", Pattern);
            else if (!string.IsNullOrEmpty(Browser))
                return string.Format("You forgot to select a Browser for '{0}' rule.", Pattern);
            else
                return "";
        }

        //-------------------------------------------------------------------------------------------------------------
        public bool isValid()
        //-------------------------------------------------------------------------------------------------------------
        {
            try //because they may be null
            {
                return Type.Length > 0 && Browser.Length > 0 && Pattern.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public int CompareTo(object obj)
        //-------------------------------------------------------------------------------------------------------------
        {
            string compareType = ((RuleModel)obj).Type;
            string comparePattern = ((RuleModel)obj).Pattern;

            if ((Pattern == "*") && (comparePattern == "*"))
                return 0;
            else if ((Pattern.Contains("*")) && (comparePattern.Contains("*")))
                return Pattern.CompareTo(comparePattern);
            else if (comparePattern == "*") 
                return -1;
            else if (comparePattern.Contains("*")) 
                return 0;
            else 
                return Type.CompareTo(compareType);
        }
    }
}
