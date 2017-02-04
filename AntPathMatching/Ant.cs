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
        private static readonly Lazy<IDictionary<string, string>> segments =
            new Lazy<IDictionary<string, string>>(CreateSegments);

        private readonly Regex regex;

        /// <summary>
        /// Initializes a new <see cref="Ant"/>.
        /// </summary>
        /// <param name="pattern">Ant-style pattern.</param>
        public Ant(string pattern)
        {
            regex = new Regex(
                TransformPattern(pattern ?? string.Empty),
                RegexOptions.Singleline
            );
        }

        /// <inheritdoc/>
        public bool IsMatch(string input) => regex.IsMatch(NormalizePath(input));

        private static string TransformPattern(string pattern)
        {
            pattern = NormalizePath(pattern);

            foreach (var kvp in segments.Value)
            {
                pattern = pattern.Replace(kvp.Key, kvp.Value);
            }

            return $"^{pattern}$";
        }

        private static string NormalizePath(string txt) =>
            txt.Replace(@"\", "/").Trim('/');

        private static IDictionary<string, string> CreateSegments() =>
            new Dictionary<string, string>
            {
                { ".", @"\." },
                { "**", @"(.+)" },
                { "*",  @"([^/]+)" },
                { "?", @"(.)" },
                { "{", "(" },
                { "}", ")" },
                { ",", "|" },
            };
    }
}
