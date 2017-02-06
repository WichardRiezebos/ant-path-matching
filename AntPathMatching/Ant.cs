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

        /// <summary>
        /// Validates whether the input matches the given pattern.
        /// </summary>
        /// <param name="input">Path for which to check if it matches the ant-pattern.</param>
        /// <returns>Whether the input matches the pattern.</returns>
        /// <inheritdoc/>
        public bool IsMatch(string input) => regex.IsMatch(GetUnixPath(input));

        private static string EscapeAndReplace(string pattern)
        {
            var unix = GetUnixPath(pattern);

            if (unix.EndsWith("/"))
            {
                unix += "**";
            }

            pattern = Regex.Escape(unix)
                .Replace(@"/\*\*/", "(.*[/])")
                .Replace(@"\*\*/", "(.*)")
                .Replace(@"/\*\*", "(.*)")
                .Replace(@"\*", "([^/]*)")
                .Replace(@"\?", "(.)")
                .Replace(@"}", ")")
                .Replace(@"\{", "(")
                .Replace(@",", "|");

            return $"^{pattern}$";
        }

        private static string GetUnixPath(string txt) => 
            txt.Replace(@"\", "/").TrimStart('/');
    }
}
