using YesserCalculator.Extension;

namespace ExtensionTemplate;

public class Extension : IExtension
{
    public string Id
        => ""; // TODO: Paste GUID here

    public string DisplayName
        => "ExtensionTemplate";

    public IEnumerable<IOperation> GetOperationList()
    {
        return [new Power()];
    }
}