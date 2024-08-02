using YesserCalculatorExtension;

namespace ExampleExtension;

public class Extension : IExtension
{
    public IEnumerable<IOperation> GetOperationList()
    {
        return [new Power()];
    }
}