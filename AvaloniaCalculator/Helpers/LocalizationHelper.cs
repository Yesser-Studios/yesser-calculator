using System.Threading;

namespace AvaloniaCalculator.Helpers;

public static class LocalizationHelper
{
    public static string DecimalSeparator
        => Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
}