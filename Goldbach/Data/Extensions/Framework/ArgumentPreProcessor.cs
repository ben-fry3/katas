using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Goldbach.Data.Extensions.Framework
{
    /// <summary>
    /// Contains extension methods to assist working with arguments passed from the command line
    /// </summary>
    public static class ArgumentPreProcessor
    {
        private static Regex _argumentCatcher = new Regex(@"^[-\/](\w+)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Ensures that any passed argument posesses one of the accepted prefixes
        /// </summary>
        /// <param name="arg">Argument to parse</param>
        /// <returns>The value of the argument, less its prefix if valid; null if otherwise</returns>
        public static string ParseArgument(this string arg)
        {
            var match = _argumentCatcher.Match(arg);

            if(match.Success)
            {
                //Group 0 is original string
                return match.Groups[1].ToString();
            }

            return null;
        }

        /// <summary>
        /// Parses a whole set of arguments, according to the <see cref="ParseArgument(string)"/> method.
        /// </summary>
        /// <param name="args">Array of strings, representing all arguments to be parsed</param>
        /// <returns>An array of steings, or null, representing the parsed arguments</returns>
        public static string[] ParseAllArguments(this string[] args)
        {
            var parsedStrings = new List<string>();

            foreach(var arg in args)
            {
                parsedStrings.Add(arg.ParseArgument());
            }

            return parsedStrings.ToArray();
        }

        /// <summary>
        /// Parses a set of arguments, and then searches for any of a second set of arguments within that intital set
        /// </summary>
        /// <param name="args">Array of strings, representing the set of arguments that should be searched in</param>
        /// <param name="checkArguments">Array of strings, containing any arguments - with no prefix - that should be searched for</param>
        /// <returns>Boolean value, representing if any of the checkArguments were founf in the args array</returns>
        public static bool CheckFor(this string[] args, string[] checkArguments)
        {
            var processedArgs = args.ParseAllArguments();

            return processedArgs.Intersect(checkArguments).Any();
        }
    }
}
