using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using YesserCalculator.Helpers;
using YesserCalculator.Models;
using YesserCalculator.Models.Operations;
using YesserCalculator.Utilities;
using YesserCalculator.Extension;
using YesserCalculator.Extension.Utilities;

namespace YesserCalculator.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    private double _number1;
    private double _number2;
    private double _result;
    private CurrentNumber _currentNumber = CurrentNumber.Number1;
    private IOperation? _currentOperation;
    private bool _appendDecimalSeparator;
    private bool _decimalSeparatorInside;
    private readonly OperationFactory _operationFactory;

    public MainWindowViewModel(OperationFactory operationFactory)
    {
        _operationFactory = operationFactory;
    }

    public string DecimalSeparator
        => LocalizationHelper.DecimalSeparator;

    public string NumberBoxContent
    {
        get
        {
            string text = GetCurrentNumberRef().ToString(CultureInfo.InvariantCulture);
            if (_appendDecimalSeparator)
                text += LocalizationHelper.DecimalSeparator;

            return text;
        }
    }

    public string OperationBoxContent
        => _currentOperation is null ? "" : _currentOperation.Symbol;

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
    
    public void NumberButton_OnClick(int number)
    {
        Console.WriteLine($"Adding number {number}");
        
        if (_currentNumber is CurrentNumber.Result)
        {
            _result = 0;
            _currentNumber = CurrentNumber.Number1;
        }
        
        var toAppend = number.ToString();

        if (_appendDecimalSeparator && !_decimalSeparatorInside)
        {
            toAppend = "." + toAppend;
            _decimalSeparatorInside = true;
            _appendDecimalSeparator = false;
        }
        
        AppendToCurrentNumber(toAppend);

        Console.WriteLine($"Current number: {GetCurrentNumberRef()}");
    }
    
    private void AppendToCurrentNumber(string toAppend)
    {
        var result = GetCurrentNumberRef().ToString(CultureInfo.InvariantCulture) + toAppend;
        GetCurrentNumberRef() = double.Parse(result);
        
        OnPropertyChanged(nameof(NumberBoxContent));
    }
    
    public void DecimalSeparator_OnClick()
    {
        if (_decimalSeparatorInside) return;
        
        _appendDecimalSeparator = true;
        OnPropertyChanged(nameof(NumberBoxContent));
    }
    
    public void OperationButton_OnClick(string symbol)
    {
        Console.WriteLine($"Processing operation: {symbol}");
        
        PreprocessNumber();

        _currentNumber = CurrentNumber.Number2;
        
        _currentOperation = _operationFactory.GetOperationFromString(symbol);
        _appendDecimalSeparator = false;
        _decimalSeparatorInside = false;
        OnPropertyChanged(nameof(NumberBoxContent));
        OnPropertyChanged(nameof(OperationBoxContent));
    }

    private void PreprocessNumber()
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
    }
    
    public void EqualsButton_OnClick()
    {
        if (_currentOperation == null)
            return;

        _result = _currentOperation.Execute(_number1, _number2);
        _currentNumber = CurrentNumber.Result;

        _number1 = 0;
        _number2 = 0;
        _currentOperation = null;
        _decimalSeparatorInside = false;
        _appendDecimalSeparator = false;
        OnPropertyChanged(nameof(NumberBoxContent));
        OnPropertyChanged(nameof(OperationBoxContent));
    }
    
    public void ClearButton_OnClick()
    {
        _number1 = 0;
        _number2 = 0;
        _result = 0;
        _currentNumber = CurrentNumber.Number1;
        _currentOperation = null;
        _appendDecimalSeparator = false;
        _decimalSeparatorInside = false;
        OnPropertyChanged(nameof(NumberBoxContent));
        OnPropertyChanged(nameof(OperationBoxContent));
    }
    
    public void BackspaceButton_OnClick()
    {
        if (_appendDecimalSeparator)
            _appendDecimalSeparator = false;
        else
        {
            var number = GetCurrentNumberRef().ToString(CultureInfo.InvariantCulture);
            number = number[..^1]; // Remove last character
            
            if (number.Last() == '.')
            {
                number = number[..^1];
                _appendDecimalSeparator = true;
                _decimalSeparatorInside = false;
            }

            var didParse = double.TryParse(number, out var parsed);
            if (!didParse) parsed = 0;

            GetCurrentNumberRef() = parsed;
        }
        
        OnPropertyChanged(nameof(NumberBoxContent));
    }

    public void ViewSourceCode_OnClick()
    {
        UrlOpener.OpenUrl("https://github.com/yesseruser/avalonia-calculator");
    }

    public void OpenExtensionsDirectory_OnClick()
    {
        try
        {
            ProcessStartInfo startInfo = new(AppDataProvider.ExtensionDirectoryPath)
            {
                UseShellExecute = true
            };
            
            Process.Start(startInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to open extension directory: {e}");
        }
    }
#pragma warning restore CA1822 // Mark members as static
}