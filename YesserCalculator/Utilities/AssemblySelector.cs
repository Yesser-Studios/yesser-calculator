using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace YesserCalculator.Utilities;

public static class AssemblySelector
{
    public static async Task<IEnumerable<Assembly>> SelectAssemblies(IStorageProvider storageProvider, string? message)
    {
        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = message,
            AllowMultiple = true,
            FileTypeFilter = new [] {new FilePickerFileType("dll")}
        });

        List<Assembly> assemblies = [];
        foreach (var file in files)
        {
            assemblies.Add(Assembly.LoadFile(file.Path.AbsolutePath));
        }

        return assemblies;
    }
}