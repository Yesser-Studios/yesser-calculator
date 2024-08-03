using YesserCalculator.Extension;

namespace ExtensionTemplate;

public class Extension : IExtension
{
    public string Id
        => "f0581238-911f-49a0-97e1-4498ff642ae0";

    public string DisplayName
        => "Example Extension";

    public IEnumerable<IOperation> GetOperationList()
    {
        return [new Power()];
    }
}