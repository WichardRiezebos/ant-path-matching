namespace AntPathMatching
{
    /// <summary>
    /// Represents an interface to create <see cref="IAnt"/> instances. Mainly used for DI frameworks.
    /// </summary>
    public interface IAntFactory
    {
        /// <summary>
        /// Creates a new <see cref="IAnt"/>.
        /// </summary>
        /// <param name="pattern">Ant-style pattern.</param>
        IAnt CreateNew(string pattern);
    }
}
