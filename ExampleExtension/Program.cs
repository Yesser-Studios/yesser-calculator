using System.Reflection;
using YesserCalculatorExtension;

namespace ExampleExtension;

public static class Program
{
    private static void Main(string[] args)
    {
        Installer.StartInstallation(Assembly.GetAssembly(typeof(Extension))!, new Extension().DisplayName,  out _);
    }
}