using System.Net;
using System.Reflection;
using YesserCalculatorExtension;
using YesserCalculatorExtension.Exceptions;
using YesserCalculatorExtension.Utilities;

namespace ExampleExtension;

public static class Program
{
    private static void Main(string[] args)
    {
        Installer.StartInstallation(Assembly.GetExecutingAssembly(), out _);
    }
}