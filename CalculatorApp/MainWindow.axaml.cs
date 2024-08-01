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
    private double _result = 0;
    private CurrentNumber _currentNumber = CurrentNumber.Number1;
    private IOperation? _currentOperation = null;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdateOperationBox()
    {
        if (_currentOperation is null)
            return;
        
        OperationBox.Text = _currentOperation.Symbol;
    }
    
    private void UpdateNumberBox()
    {
        NumberBox.Text = GetCurrentNumberRef().ToString(CultureInfo.InvariantCulture);
    }
    
    private ref double GetCurrentNumberRef()
    {
        // ReSharper disable once ConvertSwitchStatementToSwitchExpression
        switch (_currentNumber)
        {
            case CurrentNumber.Number1:
                return ref _number1;
            case CurrentNumber.Number2:
                return ref _number2;
            case CurrentNumber.Result:
                return ref _result;
            default:
                return ref _number1;
        }
    }
    
    private void NumberButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_currentNumber is CurrentNumber.Result)
        {
            _result = 0;
            _currentNumber = CurrentNumber.Number1;
        }
        
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
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (_currentNumber)
        {
            case CurrentNumber.Number2 when _currentOperation is null:
                throw new NullReferenceException("Current number is #2 but current operation is null.");
            case CurrentNumber.Number2:
                if (_number2 == 0)
                    break;
                _number1 = _currentOperation.Execute(_number1, _number2);
                _number2 = 0;
                break;
            case CurrentNumber.Result:
                _number1 = _result;
                _number2 = 0;
                _result = 0;
                break;
            case CurrentNumber.Number1:
                break;
        }

        _currentNumber = CurrentNumber.Number2;

        var tag = (sender as Button)?.Tag?.ToString();
        var factory = new OperationFactory();
        _currentOperation = factory.GetOperationFromString(tag);
        
        UpdateOperationBox();
    }

    private void Comma_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void EqualsButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_currentOperation == null)
            return;

        _result = _currentOperation.Execute(_number1, _number2);
        _currentNumber = CurrentNumber.Result;

        _number1 = 0;
        _number2 = 0;
        _currentOperation = null;
        
        UpdateNumberBox();
        UpdateOperationBox();
    }
}