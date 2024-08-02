using System.Threading;

namespace YesserCalculator.Helpers;

public static class LocalizationHelper
{
    public static string DecimalSeparator
        => Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
}