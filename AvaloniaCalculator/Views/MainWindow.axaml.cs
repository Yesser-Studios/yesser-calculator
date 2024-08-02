using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaCalculator.Models.Operations;
using AvaloniaCalculator.ViewModels;

namespace AvaloniaCalculator.Views;

public partial class MainWindow : Window
{
    public MainWindow(OperationFactory operationFactory)
    {
        InitializeComponent();
        
        int rowCount = 5;
        int columnCount = (int)Math.Ceiling(operationFactory.OperationMap.Count / (double)rowCount);
        for (int i = 0; i < columnCount; i++) 
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(100)));

        int row = 0;
        int column = MainGrid.ColumnDefinitions.Count - columnCount;
        foreach (var (symbol, operation) in operationFactory.OperationMap)
        {
            var button = new Button
            {
                CommandParameter = symbol,
                Content = symbol
            };

            button.Click += OperationButton_Click;

            button.SetValue(Grid.RowProperty, row);
            button.SetValue(Grid.ColumnProperty, column);

            row++;
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
}