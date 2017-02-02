namespace AntPathMatching
{
    /// <summary>
    /// Represents an interface which defines the ant-pattern matching basics.
    /// </summary>
    public interface IAnt
    {
        /// <summary>
        /// Validates if the input matches the given pattern.
        /// </summary>
        /// <param name="input">Text that needs to be validated.</param>
        /// <returns>If the input matches the pattern.</returns>
        bool IsMatch(string input);
    }
}