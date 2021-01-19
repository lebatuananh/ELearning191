using System;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ToSlug(this string value, int? maxLength = null)
        {
            return GenerateSlug(value, maxLength);
        }

        private static string GenerateSlug(string value, int? maxLength = null)
        {
            var result = value.RemoveDiacritics().Replace("-", " ").ToLowerInvariant();

            result = Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty);
            result = Regex.Replace(result, @"\s+", " ").Trim();

            if (maxLength.HasValue)
                result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

            return Regex.Replace(result, @"\s", "-");
        }
        public static string RemoveDiacritics(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            try
            {
                str = str.ToLower().Trim();
                str = Regex.Replace(str, @"[\r|\n]", " ");
                str = Regex.Replace(str, @"\s+", " ");
                str = Regex.Replace(str, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                str = Regex.Replace(str, "[éèẻẽẹêếềểễệ]", "e");
                str = Regex.Replace(str, "[iíìỉĩị]", "i");
                str = Regex.Replace(str, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                str = Regex.Replace(str, "[úùủũụưứừửữự]", "u");
                str = Regex.Replace(str, "[yýỳỷỹỵ]", "y");
                str = Regex.Replace(str, "[đ]", "d");
                str = Regex.Replace(str, "[\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\]", "");
                str = str.Replace("̀", "").Replace("̣", "").Replace("̉", "").Replace("̃", "").Replace("́", "");
                return str;
            }
            catch (Exception)
            {
                return str;
            }
        }
    }
}
