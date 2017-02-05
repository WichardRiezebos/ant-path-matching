using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AntPathMatching
{
    /// <summary>
    /// Represents a class which matches paths using ant-style path matching.
    /// </summary>
    [DebuggerDisplay("Pattern = {regex}")]
    public class Ant : IAnt
    {
        private readonly Regex regex;

        /// <summary>
        /// Initializes a new <see cref="Ant"/>.
        /// </summary>
        /// <param name="pattern">Ant-style pattern.</param>
        public Ant(string pattern)
        {
            regex = new Regex(
                EscapeAndReplace(pattern ?? string.Empty),
                RegexOptions.Singleline
            );
        }

        /// <inheritdoc/>
        public bool IsMatch(string input) => regex.IsMatch(GetUnixPath(input));

        private static string EscapeAndReplace(string pattern)
        {
            pattern = Regex.Escape(GetUnixPath(pattern))
                .Replace(@"/\*\*/", "(.*)")
                .Replace(@"\*\*/", "(.*)")
                .Replace(@"/\*\*", "(.*)")
                .Replace(@"\*\*", "(.*)")
                .Replace(@"\*", "([^/]*)")
                .Replace(@"\?", "(.)")
                .Replace(@"}", ")")
                .Replace(@"\{", "(")
                .Replace(@",", "|");

            return $"^{pattern}$";
        }

        private static string GetUnixPath(string txt) =>
            txt.Replace(@"\", "/").Trim('/');
    }
}
