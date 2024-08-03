using System.Reflection;
using ExtensionTemplate;
using YesserCalculator.Extension;

namespace ExtensionTemplate;

public static class Program
{
    private static void Main(string[] args)
    {
        Installer.StartInstallation(Assembly.GetAssembly(typeof(Extension))!, new Extension().DisplayName,  out _);
    }
}