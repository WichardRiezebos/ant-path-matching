namespace AntPathMatching
{
    /// <summary>
    /// Represents an interface which defines the ant-pattern matching basics.
    /// </summary>
    public interface IAnt
    {
        /// <summary>
        /// Validates whether the input matches the given pattern.
        /// </summary>
        /// <param name="input">Path for which to check if it matches the ant-pattern.</param>
        /// <returns>Whether the input matches the pattern.</returns>
        bool IsMatch(string input);
    }
}