using System.Reflection;
using YesserCalculatorExtension;

namespace ExampleExtension;

public static class Program
{
    private static void Main(string[] args)
    {
        Installer.StartInstallation(Assembly.GetExecutingAssembly(), out _);
    }
}