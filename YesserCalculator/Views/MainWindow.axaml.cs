using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using YesserCalculator.Extension;
using YesserCalculator.Models.Operations;
using YesserCalculator.Utilities;
using YesserCalculator.ViewModels;

namespace YesserCalculator.Views;

public partial class MainWindow : Window
{
    public MainWindow(OperationFactory operationFactory)
    {
        InitializeComponent();
        
        int rowCount = 5;
        int columnCount = (int)Math.Ceiling((operationFactory.OperationMap.Count - 4) / (double)rowCount);
        
        for (int i = 0; i < columnCount; i++) 
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(100)));

        int row = 0;
        int column = MainGrid.ColumnDefinitions.Count - columnCount;
        foreach (var (symbol, operation) in operationFactory.OperationMap)
        {
            var button = new Button
            {
                CommandParameter = symbol,
                Content = operation.DisplaySymbol
            };

            button.Click += OperationButton_Click;

            switch (symbol)
            {
                case "+":
                    button.SetValue(Grid.RowProperty, 1);
                    button.SetValue(Grid.ColumnProperty, 3);
                    break;
                case "-":
                    button.SetValue(Grid.RowProperty, 2);
                    button.SetValue(Grid.ColumnProperty, 3);
                    break;
                case "*":
                    button.SetValue(Grid.RowProperty, 3);
                    button.SetValue(Grid.ColumnProperty, 3);
                    break;
                case "/":
                    button.SetValue(Grid.RowProperty, 3);
                    button.SetValue(Grid.ColumnProperty, 2);
                    break;
                default:
                    button.SetValue(Grid.RowProperty, row);
                    button.SetValue(Grid.ColumnProperty, column);
                    row++;
                    break;
            }

            if (row >= rowCount)
            {
                row = 0;
                column++;
            }

            MainGrid.Children.Add(button);
        }
    }

    private void OperationButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button
            || string.IsNullOrWhiteSpace(button.CommandParameter as string))
            return;
        
        (DataContext as MainWindowViewModel)?.OperationButton_OnClick((button.CommandParameter as string)!);
    }

    private async void InstallExtensionMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = GetTopLevel(this);
        var assemblies = await AssemblySelector.SelectAssemblies(topLevel!.StorageProvider,
            "Select extension/s to install...");
        
        foreach (var assembly in assemblies)
        {
            Installer.TryInstallExtension(assembly, out var exception, true);
            if (exception != null)
                Console.WriteLine(exception);
        }
    }
}