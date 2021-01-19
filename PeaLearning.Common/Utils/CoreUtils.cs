using System;
using System.Collections.Generic;
using System.Text;

namespace PeaLearning.Common.Utils
{
    public class CoreUtils
    {
        public static string GetCurrentURL(string url, int? pageIndex)
        {
            if (url.IndexOf("/p" + pageIndex) != -1)
            {
                if (url.Substring(url.Length - (pageIndex.ToString().Length + 2)) == "/p/" + pageIndex) return url.Substring(0, url.Length - (pageIndex.ToString().Length + 2));
            }
            return url;
        }
    }
}
