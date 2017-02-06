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
        /// <param name="includeDirectoryPath">Indicates if the returned paths must include the directory.</param>
        /// <returns>Collection of matching files.</returns>
        IEnumerable<string> SearchRecursively(string directory, bool includeDirectoryPath = false);
    }
}