using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using System.Globalization;

namespace Mtf.Maui.Controls;

public partial class NumericUpDownWithLabel : ContentView
{
    private bool isPressed;
    private const int RepeatRate = 100;

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(NumericUpDownWithLabel), String.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(NumericUpDownWithLabel), 0.0, BindingMode.TwoWay);

    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(double), typeof(NumericUpDownWithLabel), 0.0);

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(double), typeof(NumericUpDownWithLabel), 100.0);

    public static readonly BindableProperty IncrementProperty =
        BindableProperty.Create(nameof(Increment), typeof(double), typeof(NumericUpDownWithLabel), 1.0);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set
        {
            var newValue = Math.Clamp(value, Minimum, Maximum);
            if (newValue != (double)GetValue(ValueProperty))
            {
                SetValue(ValueProperty, newValue);
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(newValue));
            }
        }
    }

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public double Increment
    {
        get => (double)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }

    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    public NumericUpDownWithLabel() => InitializeComponent();

    private async void OnIncrementPressed(object sender, EventArgs e)
    {
        isPressed = true;
        await StartValueChange(true).ConfigureAwait(true);
    }

    private async void OnDecrementPressed(object sender, EventArgs e)
    {
        isPressed = true;
        await StartValueChange(false).ConfigureAwait(true);
    }

    private async Task StartValueChange(bool isIncrementing)
    {
        while (isPressed)
        {
            if (isIncrementing && Value + Increment <= Maximum)
            {
                Value += Increment;
            }
            else if (!isIncrementing && Value - Increment >= Minimum)
            {
                Value -= Increment;
            }

            ValueLabel.Text = Value.ToString(CultureInfo.InvariantCulture);
            await Task.Delay(RepeatRate).ConfigureAwait(true);
        }
    }

    private void OnButtonReleased(object sender, EventArgs e) => isPressed = false;

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var newValue = e.NewTextValue;
        var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (!Double.TryParse(newValue, NumberStyles.Any, CultureInfo.CurrentCulture, out var value))
        {
            ((Entry)sender).Text = String.Concat(newValue.Where(c => Char.IsDigit(c) || c.ToString() == decimalSeparator));
        }
        else
        {
            if (value < Minimum)
            {
                ((Entry)sender).Text = Minimum.ToString(CultureInfo.InvariantCulture);
                var message = new ShowErrorMessage($"Value must be greater than or equal to {Minimum}");
                _ = WeakReferenceMessenger.Default.Send(message);
            }
            else if (value > Maximum)
            {
                ((Entry)sender).Text = Maximum.ToString(CultureInfo.InvariantCulture);
                var message = new ShowErrorMessage($"Value must be less than or equal to {Maximum}");
                _ = WeakReferenceMessenger.Default.Send(message);
            }
        }
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (Double.TryParse(ValueLabel.Text, out var enteredValue))
        {
            Value = enteredValue < Minimum ? Minimum : enteredValue > Maximum ? Maximum : enteredValue;
        }
        ValueLabel.Text = Value.ToString(CultureInfo.InvariantCulture);
    }
}

public class ValueChangedEventArgs(double newValue) : EventArgs
{
    public double NewValue { get; } = newValue;
}

public class ValueChangedEventArgsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is ValueChangedEventArgs args ? args.NewValue : (object?)null;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}