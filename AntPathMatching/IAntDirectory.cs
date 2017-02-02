using System.Collections.Generic;

namespace AntPathMatching
{
    /// <summary>
    /// Represents an interface which defines the ant-pattern matching basics for directories.
    /// </summary>
    public interface IAntDirectory
    {
        /// <summary>
        /// Searches all the files in the given directory using the ant-style pattern.
        /// </summary>
        /// <param name="directory">Path to directory to search in.</param>
        /// <returns>Collection of matching files.</returns>
        IReadOnlyCollection<string> SearchRecursively(string directory);
    }
}