using System.Collections.Generic;

namespace AvaloniaCalculator.Models.Operations;

public class OperationFactory
{
    private readonly List<IOperation> _operations = [];

    public void RegisterOperation(IOperation operation)
    {
        _operations.Add(operation);
    }
    
    public IOperation? GetOperationFromString(string? symbol)
    {
        if (symbol is null)
            return null;

        return _operations.Find(x => x.Symbol == symbol);
    }
}