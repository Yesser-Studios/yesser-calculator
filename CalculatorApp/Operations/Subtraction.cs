namespace CalculatorApp.Operations;

public class Addition : IOperation
{
    public double Execute(double number1, double number2)
        => number1 + number2;
}