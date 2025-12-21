using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Messages;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class NumericUpDownWithLabel : ContentView
{
    private bool isRunning;
    private bool isPressed;
    private const int RepeatRate = 100;
    private const int InternalPrecision = 6;

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(NumericUpDownWithLabel), String.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(NumericUpDownWithLabel), 0.0, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(double), typeof(NumericUpDownWithLabel), 0.0);

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(double), typeof(NumericUpDownWithLabel), 100.0);

    public static readonly BindableProperty IncrementProperty =
        BindableProperty.Create(nameof(Increment), typeof(double), typeof(NumericUpDownWithLabel), 1.0);

    public static readonly BindableProperty DecimalPlacesProperty =
        BindableProperty.Create(nameof(DecimalPlaces), typeof(int), typeof(NumericUpDownWithLabel), -1, propertyChanged: OnDecimalPlacesChanged);
    
    public static readonly BindableProperty IncrementCommandProperty =
        BindableProperty.Create(nameof(IncrementCommand), typeof(ICommand), typeof(NumericUpDownWithLabel));

    public static readonly BindableProperty DecrementCommandProperty =
        BindableProperty.Create(nameof(DecrementCommand), typeof(ICommand), typeof(NumericUpDownWithLabel));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
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

    public int DecimalPlaces
    {
        get => (int)GetValue(DecimalPlacesProperty);
        set => SetValue(DecimalPlacesProperty, value);
    }

    public ICommand? IncrementCommand
    {
        get => (ICommand?)GetValue(IncrementCommandProperty);
        set => SetValue(IncrementCommandProperty, value);
    }

    public ICommand? DecrementCommand
    {
        get => (ICommand?)GetValue(DecrementCommandProperty);
        set => SetValue(DecrementCommandProperty, value);
    }


    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    public NumericUpDownWithLabel()
    {
        InitializeComponent();
        UpdateEntryText(true);
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NumericUpDownWithLabel control)
        {
            control.ValueChanged?.Invoke(control, new ValueChangedEventArgs((double)oldValue, (double)newValue));
            control.UpdateEntryText(false);
        }
    }

    private static void OnDecimalPlacesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NumericUpDownWithLabel control)
        {
            control.UpdateEntryText(true);
        }
    }

    private void UpdateEntryText(bool force)
    {
        if (ValueLabel == null)
        {
            return;
        }

        if (!force && ValueLabel.IsFocused)
        {
            return;
        }

        ValueLabel.Text = Value.ToString(GetFormatString(), CultureInfo.CurrentCulture);
    }

    private string GetFormatString()
    {
        if (DecimalPlaces < 0)
        {
            return (Increment % 1 == 0) ? "0" : "0.##";
        }
        return $"F{DecimalPlaces}";
    }

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
        if (isRunning)
        {
            return;
        }
        isRunning = true;

        try
        {
            if (ValueLabel.IsFocused)
            {
                ValueLabel.Unfocus();
            }

            while (isPressed)
            {
                if (!IsLoaded || !IsVisible || !IsEnabled)
                {
                    isPressed = false;
                    break;
                }

                var command = isIncrementing ? IncrementCommand : DecrementCommand;
                if (command != null)
                {
                    command.Execute(null);
                }
                else
                {
                    double newValue = isIncrementing ? Value + Increment : Value - Increment;
                    newValue = Math.Round(newValue, InternalPrecision);

                    if (newValue >= Minimum && newValue <= Maximum)
                    {
                        Value = newValue;
                    }
                    else if (isIncrementing && Value < Maximum)
                    {
                        Value = Maximum;
                    }
                    else if (!isIncrementing && Value > Minimum)
                    {
                        Value = Minimum;
                    }

                    UpdateEntryText(true);
                }

                await Task.Delay(RepeatRate).ConfigureAwait(true);
            }
        }
        finally
        {
            isRunning = false;
        }
    }

    private void OnButtonReleased(object sender, EventArgs e) => isPressed = false;

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!((Entry)sender).IsFocused)
        {
            return;
        }

        var text = e.NewTextValue;

        if (String.IsNullOrEmpty(text) || text == "-" || text == "." || text == ",")
        {
            //Value = Minimum;
            return;
        }

        if (Double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out var result) ||
            Double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            Value = Math.Clamp(result, Minimum, Maximum);
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        ValidateAndCommitValue();
        ValueLabel.Unfocus();
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        ValidateAndCommitValue();
    }

    private void ValidateAndCommitValue()
    {
        var text = ValueLabel.Text;

        if (Double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out var result) ||
            Double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            var clamped = Math.Clamp(result, Minimum, Maximum);

            if (Math.Abs(clamped - result) > 0.0001)
            {
                var msg = result < Minimum ? $"Value must be >= {Minimum}" : $"Value must be <= {Maximum}";
                _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(msg));
            }

            Value = Math.Round(clamped, InternalPrecision);
        }

        UpdateEntryText(true);
    }

    private void Button_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            isPressed = false;
        }
    }
}