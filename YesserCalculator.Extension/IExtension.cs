namespace YesserCalculator.Extension;

/// <summary>
    /// <para>
        /// The Extension interface.
    /// </para>
    /// <para>
        /// All extensions meant to be loaded directly by Yesser Calculator should be named exactly "Extension".
    /// </para>
/// </summary>
public interface IExtension
{
    public string Id { get; }
    
    public string DisplayName { get; }
    
    public IEnumerable<IOperation> GetOperationList();
}