namespace AvaloniaCalculatorExtension;

/// <summary>
    /// <para>
        /// The Extension interface.
    /// </para>
    /// <para>
        /// All extensions meant to be loaded directly by Avalonia Calculator should be named exactly "Extension".
    /// </para>
/// </summary>
public interface IExtension
{
    public IEnumerable<IOperation> GetOperationList();
}