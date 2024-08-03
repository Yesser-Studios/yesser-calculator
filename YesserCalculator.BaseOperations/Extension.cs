using YesserCalculatorExtension;

namespace YesserCalculator.BaseOperations;

public class Extension : IExtension
{
    public string Id
        => "a6747a83-a51c-4a4d-9dd6-70086a8a4b3a";

    public IEnumerable<IOperation> GetOperationList()
    {
        return [new Addition(), new Subtraction(), new Multiplication(), new Division()];
    }
}