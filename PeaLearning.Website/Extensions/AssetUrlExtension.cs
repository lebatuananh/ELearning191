using PeaLearning.Common;
using System.Text.RegularExpressions;

namespace PeaLearning.Website.Extensions
{
    public static class AssetUrlExtension
    {
        public static string GetUrl(this string url)
        {
            return !url.Contains(StaticVariable.DomainImage) && !new Regex("^(http|https)://").IsMatch(url) ? string.Concat(StaticVariable.DomainImage, url) : url;
        }
    }
}
