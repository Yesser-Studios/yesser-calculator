using YesserCalculator.Extension;

namespace YesserCalculator.BaseOperations;

public class Multiplication : IOperation
{
    public double Execute(double number1, double number2)
        => number1 * number2;
    
    public string Symbol => "*";
    public string DisplaySymbol => "×";
}