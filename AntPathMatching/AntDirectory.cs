using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AntPathMatching
{
    /// <summary>
    /// Represents a class used to match files recusively with ant-style pattern.
    /// </summary>
    public class AntDirectory : IAntDirectory
    {
        private readonly IAnt ant;

        /// <summary>
        /// Initializes a new <see cref="AntDirectory"/>.
        /// </summary>
        /// <param name="ant">Ant pattern used for directory-searching.</param>
        /// <exception cref="ArgumentNullException">Throw when <paramref name="ant"/>is null.</exception>
        public AntDirectory(IAnt ant)
        {
            if (ant == null) throw new ArgumentNullException(nameof(ant));

            this.ant = ant;
        }

        /// <inheritDoc />
        public IEnumerable<string> SearchRecursively(string directory)
        {
            var files = Directory
                .GetFiles(directory, "*", SearchOption.AllDirectories)
                .Select(f => f.Replace(directory, string.Empty))
                .ToList();

            return files
                .Where(ant.IsMatch)
                .ToList();
        }
    }
}
