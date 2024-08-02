using System;
using System.Collections.Generic;
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
            var extensionType = assembly.GetType("Extension");
            if (extensionType == null)
                return false;
            
            IExtension? instanceOfExtensionType = Activator.CreateInstance(extensionType) as IExtension;

            if (instanceOfExtensionType == null)
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