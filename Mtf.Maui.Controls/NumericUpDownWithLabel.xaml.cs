using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Extensions;
using Mtf.Maui.Controls.Messages;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class NumericUpDownWithLabel : ContentView, IDisposable
{
    private CancellationTokenSource? repeatIncreaseCts;
    private CancellationTokenSource? repeatDecreaseCts;
    private const int RepeatRate = 100;
    private const int InternalPrecision = 6;

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(NumericUpDownWithLabel), String.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(NumericUpDownWithLabel), 0.0, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(double), typeof(NumericUpDownWithLabel), 0.0, propertyChanged: OnMinimumChanged);

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(double), typeof(NumericUpDownWithLabel), 100.0, propertyChanged: OnMaximumChanged);

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

    private void OnIncrementPressed(object s, EventArgs e) => StartRepeat(true);

    private void OnIncrementReleased(object s, EventArgs e) => StopIncrease();

    private void OnDecrementPressed(object s, EventArgs e) => StartRepeat(false);

    private void OnDecrementReleased(object s, EventArgs e) => StopDecrease();

    private void IncreaseButton_Unfocused(object sender, FocusEventArgs e) => StopIncrease();

    private void DecreaseButton_Unfocused(object sender, FocusEventArgs e) => StopDecrease();

    private void StartRepeat(bool isIncrementing)
    {
        if (isIncrementing)
        {
            StopDecrease();

            repeatIncreaseCts?.Cancel();
            repeatIncreaseCts?.Dispose();
            repeatIncreaseCts = new CancellationTokenSource();
            _ = StartValueChangeLoopAsync(isIncrementing, repeatIncreaseCts);
        }
        else
        {
            StopIncrease();

            repeatDecreaseCts?.Cancel();
            repeatDecreaseCts?.Dispose();
            repeatDecreaseCts = new CancellationTokenSource();
            _ = StartValueChangeLoopAsync(isIncrementing, repeatDecreaseCts);
        }
    }

    private async Task StartValueChangeLoopAsync(bool isIncrementing, CancellationTokenSource cancellationTokenSource)
    {
        try
        {
            var token = cancellationTokenSource.Token;
            if (ValueLabel.IsFocused)
            {
                MainThread.BeginInvokeOnMainThread(() => ValueLabel.Unfocus());
            }

            while (!token.IsCancellationRequested)
            {
                if (!IsLoaded || !IsVisible || !IsEnabled)
                {
                    break;
                }
                if (Value <= Minimum && !isIncrementing)
                {
                    Value = Minimum;
                    break;
                }
                if (Value >= Maximum && isIncrementing)
                {
                    Value = Maximum;
                    break;
                }

                var command = isIncrementing ? IncrementCommand : DecrementCommand;
                if (command != null)
                {
                    command.Execute(null);
                }
                else
                {
                    if (Minimum > Maximum)
                    {
                        SafetyCheck();
                        break;
                    }

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
            if (isIncrementing)
            {
                StopIncrease();
            }
            else
            {
                StopDecrease();
            }
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!((Entry)sender).IsFocused)
        {
            return;
        }

        var text = e.NewTextValue;

        if (String.IsNullOrEmpty(text) || text == "-" || text == "." || text == ",")
        {
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
            if (Minimum > Maximum)
            {
                SafetyCheck();
                return;
            }

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

    private void SafetyCheck([CallerMemberName] string caller = "")
    {
        var msg = $"[{nameof(NumericUpDownWithLabel)} Warning] Invalid range: Minimum ({Minimum}) > Maximum ({Maximum}). Please fix the bounds ({caller}).";
        Debug.WriteLine(msg);
        _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(msg));
        UpdateEntryText(true);
    }

    private void IncreaseButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            StopIncrease();
        }
    }

    private void DecreaseButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            StopDecrease();
        }
    }

    private static void OnMinimumChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not NumericUpDownWithLabel control)
        {
            return;
        }

        var min = (double)newValue;
        if (min > control.Maximum)
        {
            var msg = $"[{nameof(NumericUpDownWithLabel)} Warning] Minimum ({min}) was greater than Maximum ({control.Maximum}). Auto correcting in {nameof(OnMinimumChanged)}.";
            Debug.WriteLine(msg);
            control.SetValue(MinimumProperty, control.Maximum);
        }
        if (control.Value < min)
        {
            control.Value = min;
        }

        control.UpdateEntryText(true);
    }

    private static void OnMaximumChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not NumericUpDownWithLabel control)
        {
            return;
        }

        var max = (double)newValue;
        if (max < control.Minimum)
        {
            var msg = $"[{nameof(NumericUpDownWithLabel)} Warning] Maximum ({max}) was less than Minimum ({control.Minimum}). Auto correcting in {nameof(OnMaximumChanged)}.";
            Debug.WriteLine(msg);
            control.SetValue(MaximumProperty, control.Minimum);
        }

        if (control.Value > max)
        {
            control.Value = max;
        }

        control.UpdateEntryText(true);
    }

    private void StopIncrease()
    {
        try
        {
            repeatIncreaseCts?.Cancel();
            repeatIncreaseCts?.Dispose();
        }
        catch (Exception ex)
        {
            ex.ShowError();
        }
        finally
        {
            repeatIncreaseCts = null;
        }
    }

    private void StopDecrease()
    {
        try
        {
            repeatDecreaseCts?.Cancel();
            repeatDecreaseCts?.Dispose();
        }
        catch (Exception ex)
        {
            ex.ShowError();
        }
        finally
        {
            repeatDecreaseCts = null;
        }
    }

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                repeatIncreaseCts?.Cancel();
                repeatIncreaseCts?.Dispose();
                repeatIncreaseCts = null;

                repeatDecreaseCts?.Cancel();
                repeatDecreaseCts?.Dispose();
                repeatDecreaseCts = null;
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}