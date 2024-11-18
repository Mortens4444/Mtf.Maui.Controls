using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class NumericUpDownWithLabel : ContentView
{
    private readonly NumericUpDownWithLabelViewModel numericUpDownWithLabelViewModel;

    public NumericUpDownWithLabel()
    {
        InitializeComponent();
        numericUpDownWithLabelViewModel = (NumericUpDownWithLabelViewModel)BindingContext;
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            numericUpDownWithLabelViewModel.OnEntryUnfocused(entry.Text);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e) => numericUpDownWithLabelViewModel.OnTextChanged(sender, e);

    private void OnDecrementPressed(object sender, EventArgs e) => numericUpDownWithLabelViewModel.DecrementCommand.Execute(null);

    private void OnIncrementPressed(object sender, EventArgs e) => numericUpDownWithLabelViewModel.IncrementCommand.Execute(null);

    private void OnButtonReleased(object sender, EventArgs e) => numericUpDownWithLabelViewModel.StopCommand.Execute(null);
}