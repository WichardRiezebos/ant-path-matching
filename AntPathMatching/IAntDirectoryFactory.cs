namespace AntPathMatching
{
    /// <summary>
    /// Represents an interface to create <see cref="IAntDirectory"/> instances. Mainly used for DI frameworks.
    /// </summary>
    public interface IAntDirectoryFactory
    {
        /// <summary>
        /// Initializes a new <see cref="IAntDirectory"/>.
        /// </summary>
        /// <param name="ant">Ant pattern used for directory-searching.</param>
        IAntDirectory CreateNew(IAnt ant);
    }
}
