using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class EntryWithLabel : ContentView
{
    public EntryWithLabel() => InitializeComponent();

    private void OnTextChanged(object sender, TextChangedEventArgs e) => ((EntryWithLabelViewModel)BindingContext).OnTextChanged(sender, e);
}