using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using YesserCalculator.Helpers;
using YesserCalculator.Models.Operations;
using YesserCalculator.Utilities;
using YesserCalculator.ViewModels;
using YesserCalculator.Views;
using YesserCalculatorExtension;
using YesserCalculatorExtension.Utilities;

namespace YesserCalculator;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Installer.TryInstallExtension(Assembly.GetAssembly(typeof(BaseOperations.Extension))!, out _, true);
            
            var factory = ExtensionLoader.LoadAllOperations(AppDataProvider.ExtensionDirectoryPath, out _, out _, out _);
            
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow(factory)
            {
                DataContext = new MainWindowViewModel(factory),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}