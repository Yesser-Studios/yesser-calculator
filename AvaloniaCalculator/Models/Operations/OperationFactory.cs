namespace AvaloniaCalculator.Models.Operations;

public class OperationFactory
{
    public IOperation? GetOperationFromString(string? symbol)
    {
        if (symbol is null)
            return null;
        
        return symbol switch
        {
            "+" => new Addition(),
            "-" => new Subtraction(),
            "*" => new Multiplication(),
            "/" => new Division(),
            _ => null
        };
    }
}