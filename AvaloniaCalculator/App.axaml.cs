using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaCalculator.Models.Operations;
using AvaloniaCalculator.ViewModels;
using AvaloniaCalculator.Views;

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
            factory.RegisterOperation(new Addition());
            factory.RegisterOperation(new Subtraction());
            factory.RegisterOperation(new Multiplication());
            factory.RegisterOperation(new Division());
            
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(factory),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}