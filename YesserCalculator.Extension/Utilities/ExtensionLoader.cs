using System.Reflection;
using YesserCalculator.Models.Operations;
using YesserCalculator.Extension;
using YesserCalculator.Extension.Utilities;

namespace YesserCalculator.Utilities;

public static class ExtensionLoader
{
    public static bool TryLoadExtension(string path, out IEnumerable<IOperation> operations, out Exception? exception,
        out IExtension? extension)
    {
        operations = [];
        exception = null;
        extension = null;
        
        try
        {
            var assembly = Assembly.LoadFile(path);
            var extensionTypes = assembly.GetTypes();
            var extensionType = extensionTypes.First(x => x.Name == "Extension");

            if (Activator.CreateInstance(extensionType) is not IExtension instanceOfExtensionType)
                return false;

            operations = instanceOfExtensionType.GetOperationList();
            extension = instanceOfExtensionType;

            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }

    public static OperationFactory LoadAllOperations(string directory, out IEnumerable<IExtension> extensions,
        out IEnumerable<Exception> exceptions, out IEnumerable<string> ids)
    {
        var factory = new OperationFactory();
        List<IOperation> operations = [];
        List<string> loadedIds = [];
        List<IExtension> loadedExtensions = [];
        List<Exception> exceptionList = [];

        exceptions = exceptionList;
        extensions = loadedExtensions;
        ids = loadedIds;
        
        AppDataProvider.CreateExtensionDirectory();
        
        var extensionPaths = Directory.GetFiles(directory);
        
        foreach (var path in extensionPaths)
        {
            var loaded = TryLoadExtension(path, out var newOperations,
                out var exception, out var extension);

            if (!loaded)
            {
                Console.WriteLine($"Unable to load extension: {path}. Exception: {exception}");
                if (exception != null)
                    exceptionList.Add(exception);
                continue;
            }

            if (extension == null)
            {
                Console.WriteLine("Extension assembly loaded but extension is null.");
                continue;
            }
            
            if (loadedIds.Contains(extension.Id))
            {
                Console.WriteLine($"Unable to load extension of ID {extension.Id} because this ID is already loaded.");
                continue;
            }
            
            loadedIds.Add(extension.Id);
            operations.AddRange(newOperations);
            loadedExtensions.Add(extension);
        }
        
        foreach (var operation in operations)
        {
            bool registered = factory.TryRegisterOperation(operation, out var exception);
            if (registered) continue;

            Console.WriteLine($"Failed to load: {operation.Symbol}. Exception message: {exception?.Message}");
        }

        return factory;
    }
}