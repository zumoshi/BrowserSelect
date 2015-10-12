using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BrowserSelect {
    static class IconExtractor {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern UInt32 PrivateExtractIcons(String lpszFile, int nIconIndex, int cxIcon, int cyIcon, IntPtr[] phicon, IntPtr[] piconid, UInt32 nIcons, UInt32 flags);
        static public Icon fromFile(string filename) {
            IntPtr[] phicon = new IntPtr[] { IntPtr.Zero };
            IntPtr[] piconid = new IntPtr[] { IntPtr.Zero };

            PrivateExtractIcons(filename, 0, 128, 128, phicon, piconid, 1, 0);

            if (phicon[0] != IntPtr.Zero)
                return System.Drawing.Icon.FromHandle(phicon[0]);
            return null;
        }
    }
}
