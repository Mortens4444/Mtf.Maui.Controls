using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using System.Globalization;

namespace Mtf.Maui.Controls.ViewModels;

public partial class NumericUpDownWithLabelViewModel : ObservableObject, IDisposable
{
    private volatile int disposed;
    private const int ChangeSpeed = 100;
    private CancellationTokenSource cancellationTokenSource = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanIncrement))]
    [NotifyPropertyChangedFor(nameof(CanDecrement))]
    private double value;

    [ObservableProperty]
    private double minimum = 0.0;

    [ObservableProperty]
    private double maximum = 100.0;

    [ObservableProperty]
    private double increment = 1.0;

    [ObservableProperty]
    private string label = String.Empty;

    public bool CanIncrement => Value + Increment <= Maximum;

    public bool CanDecrement => Value - Increment >= Minimum;

    partial void OnValueChanged(double oldValue, double newValue)
    {
        Value = Math.Clamp(newValue, Minimum, Maximum);
    }

    public void IncrementValue()
    {
        if (CanIncrement)
        {
            Value += Increment;
        }
    }

    public void DecrementValue()
    {
        if (CanDecrement)
        {
            Value -= Increment;
        }
    }

    [RelayCommand]
    private async Task IncrementAsync()
    {
        using (cancellationTokenSource = new CancellationTokenSource())
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                IncrementValue();
                await Task.Delay(ChangeSpeed).ConfigureAwait(false);
            }
        }
    }

    [RelayCommand]
    private async Task DecrementAsync()
    {
        using (cancellationTokenSource = new CancellationTokenSource())
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                DecrementValue();
                await Task.Delay(ChangeSpeed).ConfigureAwait(false);
            }
        }
    }

    [RelayCommand]
    private void Stop() => cancellationTokenSource.Cancel();

    ~NumericUpDownWithLabelViewModel()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Interlocked.Exchange(ref disposed, 1) != 0)
        {
            return;
        }

        if (disposing)
        {
            cancellationTokenSource.Dispose();
        }
    }

    public void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
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
                    entry.Text = Minimum.ToString(CultureInfo.InvariantCulture);
                    var message = new ShowErrorMessage($"Value must be greater than or equal to {Minimum}");
                    _ = WeakReferenceMessenger.Default.Send(message);
                }
                else if (value > Maximum)
                {
                    entry.Text = Maximum.ToString(CultureInfo.InvariantCulture);
                    var message = new ShowErrorMessage($"Value must be less than or equal to {Maximum}");
                    _ = WeakReferenceMessenger.Default.Send(message);
                }
            }
        }
    }

    public void OnEntryUnfocused(string newTextValue)
    {
        if (Double.TryParse(newTextValue, out var enteredValue))
        {
            Value = enteredValue < Minimum ? Minimum : enteredValue > Maximum ? Maximum : enteredValue;
        }
    }
}