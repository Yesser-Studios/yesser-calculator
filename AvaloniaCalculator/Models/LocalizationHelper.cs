using System.Threading;

namespace AvaloniaCalculator.Models;

public static class LocalizationHelper
{
    public static string DecimalSeparator
        => Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
}