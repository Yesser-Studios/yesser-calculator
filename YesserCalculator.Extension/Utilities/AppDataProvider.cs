namespace YesserCalculator.Extension.Utilities;

public class AppDataProvider
{
    private static string BaseAppDataPath
        => Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData,
            Environment.SpecialFolderOption.Create);
    
    public static string AppDataPath =>
        Path.Join(BaseAppDataPath, "YesserCalculator");

    public static string ExtensionDirectoryPath =>
        Path.Join(AppDataPath, "Extensions");

    public static void CreateExtensionDirectory()
    {
        if (!Directory.Exists(ExtensionDirectoryPath))
            Directory.CreateDirectory(ExtensionDirectoryPath);
    }
}