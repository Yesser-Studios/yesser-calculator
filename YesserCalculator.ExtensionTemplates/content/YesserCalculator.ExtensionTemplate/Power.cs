using YesserCalculator.Extension;

namespace ExtensionTemplate;

public class Power : IOperation
{
    public double Execute(double number1, double number2)
        => Math.Pow(number1, number2);

    public string Symbol => "^";
    public string DisplaySymbol => Symbol;
}