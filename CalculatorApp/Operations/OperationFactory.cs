namespace CalculatorApp.Operations;

public class OperationFactory
{
    public IOperation? GetOperationFromString(string type)
    {
        return type switch
        {
            "+" => new Addition(),
            "-" => new Subtraction(),
            "*" => new Multiplication(),
            "/" => new Division(),
            _ => null
        };
    }
}