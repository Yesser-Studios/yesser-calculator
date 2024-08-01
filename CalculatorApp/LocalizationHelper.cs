using System.Threading;

namespace CalculatorApp;

public static class LocalizationHelper
{
    public static string DecimalSeparator
        => Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
}