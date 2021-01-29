using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BrowserSelect
{
    //=============================================================================================================
    public class BrowserModel : IComparable
    //=============================================================================================================
    {
        public string Identifier => $"{exec} {additionalArgs}";

        public string name;
        public string exec;
        public string icon;
        public string additionalArgs = "";

        //-------------------------------------------------------------------------------------------------------------
        public static implicit operator BrowserModel(string s)
        //-------------------------------------------------------------------------------------------------------------
        {
            BrowserFinder finder = new BrowserFinder();
            return finder.FindBrowsers().First(b => b.name == s);
        }

        //-------------------------------------------------------------------------------------------------------------
        public Image string2Icon()
        //-------------------------------------------------------------------------------------------------------------
        {
            byte[] byteArray = Convert.FromBase64String(this.icon);
            Bitmap newIcon;
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                newIcon = new Bitmap(stream);
            }

            return newIcon;
        }

        //-------------------------------------------------------------------------------------------------------------
        public string privateArg
        //-------------------------------------------------------------------------------------------------------------
        {
            get
            {
                var file = exec.Split(new[] { '/', '\\' }).Last().ToLower();
                if (file.Contains("chrome") || file.Contains("msedge"))
                    return "-incognito";
                else if (file.Contains("opera"))
                    return "-newprivatetab";
                else if (file.Contains("iexplore"))
                    return "-private";
                else if (file.Contains("edge"))
                    return "-private";
                else if (file.Contains("launcher"))
                    return "-private";
                else
                    return "-private-window";  // FF
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public string profileIcon
        //-------------------------------------------------------------------------------------------------------------
        {
            get
            {
                if (name.Contains("Google Chrome"))
                    return "Google Profile.ico";
                else if (name.Contains("Microsoft Edge"))
                    return "Edge Profile.ico";
                else
                    return "";
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public List<char> shortcuts => Regex.Replace(name, @"[^A-Za-z\s]", "").Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Substring(0, 1).ToLower()[0]).ToList();

        //-------------------------------------------------------------------------------------------------------------
        public override string ToString()
        //-------------------------------------------------------------------------------------------------------------
        {
            return name;
        }

        //-------------------------------------------------------------------------------------------------------------
        public int CompareTo(object obj)
        //-------------------------------------------------------------------------------------------------------------
        {
            return name.CompareTo(((BrowserModel)obj).name);
        }
    }
}
