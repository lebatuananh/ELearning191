using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Shared.Utility
{
    public static class StringEqualityComparer
    {
        public static List<string> RemoveDuplicatesIterative(List<string> items)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                // Assume not duplicate.
                bool duplicate = false;
                items[i] = Regex.Replace(items[i], @"\s+", " ");
                for (int z = 0; z < i; z++)
                {
                    items[z] = Regex.Replace(items[z], @"\s+", " ");
                    if (items[z].Trim() == items[i].Trim())
                    {
                        // This is a duplicate.
                        duplicate = true;
                        break;
                    }
                }

                // If not duplicate, add to result.
                if (!duplicate)
                {
                    result.Add(items[i]);
                }
            }

            return result;
        }
    }
}