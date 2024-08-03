namespace YesserCalculator.Extension;

public interface IOperation
{
    public double Execute(double number1, double number2);
    public string Symbol { get; }
    public string DisplaySymbol { get; }
}
