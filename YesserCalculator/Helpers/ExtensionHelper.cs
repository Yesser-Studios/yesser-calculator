using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YesserCalculatorExtension;

namespace YesserCalculator.Helpers;

public static class ExtensionHelper
{
    public static bool TryLoadPlugin(string path, out IEnumerable<IOperation> result)
    {
        result = [];
        try
        {
            var assembly = Assembly.LoadFile(path);
            var extensionTypes = assembly.GetTypes();
            var extensionType = extensionTypes.First(x => x.Name == "Extension");

            if (Activator.CreateInstance(extensionType) is not IExtension instanceOfExtensionType)
                return false;

            result = instanceOfExtensionType.GetOperationList();

            return true;
        }
        catch
        {
            return false;
        }
    }
}