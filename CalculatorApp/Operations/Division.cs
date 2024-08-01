namespace CalculatorApp.Operations;

public class Division : IOperation
{
    public double Execute(double number1, double number2)
        => number1 / number2;
}