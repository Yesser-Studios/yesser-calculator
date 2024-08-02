using AvaloniaCalculatorExtension;

namespace AvaloniaCalculator.BaseOperations;

public class Extension : IExtension
{
    public IEnumerable<IOperation> GetOperationList()
    {
        return [new Addition(), new Subtraction(), new Multiplication(), new Division()];
    }
}