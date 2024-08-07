using System.Reflection;
using YesserCalculator.Extension.Exceptions;
using YesserCalculator.Extension.Utilities;

namespace YesserCalculator.Extension;

public static class Installer
{
    public static void StartInstallation(Assembly assembly, string displayName, out Exception? exception)
    {
        var install = AskFor($"Do you want to install extension {displayName}?");
        if (!install)
        {
            exception = new UserCancelledException("User cancelled installation");
            return;
        }
        
        Console.WriteLine("Installing extension...");
        var installed = TryInstallExtension(assembly, out exception);
        Console.WriteLine(installed ? "Successfully installed." : $"Failed to install because: {exception?.Message}");
    }

    public static bool TryInstallExtension(Assembly assembly, out Exception? exception, bool unattended = false)
    {
        exception = null;
        
        try
        {
            var assemblyPath = assembly.Location;
            var targetPath = Path.Join(AppDataProvider.ExtensionDirectoryPath,
                Path.GetFileName(assemblyPath));
            
            AppDataProvider.CreateExtensionDirectory();

            if (!unattended
                && File.Exists(targetPath))
            {
                
                var update = AskFor("Do you want to update the already installed version of this extension?");
                if (!update)
                {
                    exception = new UserCancelledException("Update cancelled by user.");
                    return false;
                }
            }

            File.Copy(assemblyPath, targetPath, true);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }

    private static bool AskFor(string question)
    {
        Console.Write($"{question} (Y/n): ");
        var line = Console.ReadLine();
        if (line == null) return AskFor(question);
        return line.ToLower() switch
        {
            "" => true,
            "y" => true,
            "n" => false,
            _ => AskFor(question)
        };
    }
}