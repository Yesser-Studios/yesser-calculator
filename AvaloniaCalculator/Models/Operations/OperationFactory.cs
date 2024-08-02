using System;
using System.Collections.Generic;
using AvaloniaCalculatorExtension;

namespace AvaloniaCalculator.Models.Operations;

public class OperationFactory
{
    public readonly Dictionary<string, IOperation> OperationMap = [];

    public void RegisterOperation(IOperation operation)
    {
        OperationMap.Add(operation.Symbol, operation);
    }

    public bool TryRegisterOperation(IOperation operation, out Exception? exception)
    {
        try
        {
            RegisterOperation(operation);
            exception = null;
            return true;
        }
        catch (Exception e)
        {
            exception = e;
            return false;
        }
    }
    
    public IOperation? GetOperationFromString(string? symbol)
    {
        if (symbol is null)
            return null;

        var exists = OperationMap.TryGetValue(symbol, out var operation);
        return exists ? operation : null;
    }
}