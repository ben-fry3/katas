using System;
using System.Collections.Generic;
using System.Text;

namespace Mines
{
    public static class Extensions
    {
        /// <summary>
        /// Breaks up a string into an IEnumerable of strings of specified length.
        /// If the input string is not an integer multiple of the specified length, then it - and
        /// consequently the last string returned - will be right-padded with whitespace
        /// </summary>
        /// <param name="str">Input string, which is to be broken up</param>
        /// <param name="len">Length of each of the parts returned</param>
        /// <returns>An enumerable of srtings</returns>
        public static IEnumerable<string> BreakToLength(this string str, int len)
        {
            if(str.Length == 0)
            {
                yield break;
            }

            var internalStr = str.Length % len == 0
                ? str
                : str.PadRight((int)Math.Ceiling((decimal)str.Length / (decimal) len) * len);

            for (var i = 0; i < internalStr.Length / len; i++)
            {
                yield return internalStr.Substring(i * len, len);
            }
        }

        /// <summary>
        /// Takes an enumerable of strings, and converts them into one single string
        /// </summary>
        /// <param name="strs">Strings to combine</param>
        /// <returns>A single string, formed of all components of the input</returns>
        public static string Combine(this IEnumerable<string> strs)
        {
            var sb = new StringBuilder();
            
            foreach(var str in strs)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }
    }
}
