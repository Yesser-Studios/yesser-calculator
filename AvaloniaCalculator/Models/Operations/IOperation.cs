namespace AvaloniaCalculator.Models.Operations;

public interface IOperation
{
    public double Execute(double number1, double number2);
    public string Symbol { get; }
}