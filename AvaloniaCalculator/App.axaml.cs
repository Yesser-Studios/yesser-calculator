using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaCalculator.Models.Operations;
using AvaloniaCalculator.ViewModels;
using AvaloniaCalculator.Views;
using AvaloniaCalculatorExtension;

namespace AvaloniaCalculator;

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
            var factory = new OperationFactory();
            List<IOperation> operations = [];
            
            var baseExtension = new BaseOperations.Extension();
            operations.AddRange(baseExtension.GetOperationList());
            
            // Add extension loading
            
            foreach (var operation in operations)
            {
                bool registered = factory.TryRegisterOperation(operation, out var exception);
                if (registered) continue;

                Console.WriteLine($"Failed to load: {operation.Symbol}. Exception message: {exception?.Message}");
            }
            
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