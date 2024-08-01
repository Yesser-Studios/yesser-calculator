using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CalculatorApp.Operations;

namespace CalculatorApp;

public partial class MainWindow : Window
{
    private double _number1 = 0;
    private double _number2 = 0;
    private bool _isNumber2Current = false;
    private IOperation? _currentOperation = null;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdateNumberBox()
    {
        NumberBox.Text = GetCurrentNumberRef().ToString(CultureInfo.InvariantCulture);
    }
    
    private ref double GetCurrentNumberRef()
    {
        if (_isNumber2Current)
            return ref _number2;
        
        return ref _number1;
    }
    
    private void NumberButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var tag = (sender as Button)?.Tag?.ToString();
        var parsed = int.TryParse(tag, out var number);
        if (!parsed)
        {
            Console.WriteLine($"Could not parse tag: {tag}");
            return;
        }

        GetCurrentNumberRef() *= 10;
        GetCurrentNumberRef() += number;
        
        UpdateNumberBox();
    }

    private void OperationButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _isNumber2Current = !_isNumber2Current;

        var tag = (sender as Button)?.Tag?.ToString();
        var factory = new OperationFactory();
        _currentOperation = factory.GetOperationFromString(tag);
    }

    private void Comma_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}