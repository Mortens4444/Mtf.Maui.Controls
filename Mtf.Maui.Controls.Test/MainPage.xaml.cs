using Mtf.Maui.Controls.Interfaces;
using System.Windows.Input;

namespace Mtf.Maui.Controls.Test;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public ICommand ToggleTextCommand { get; } = new Command((sender) =>
    {
        var labelWithToggleCommand = sender as IHaveText;
        //labelWithToggleCommand.Text = labelWithToggleCommand.Text.ChangeExpanderText();

        //var expander = (sender as ContentView)?.Parent?.Parent as Expander;
        //expander.IsExpanded = !expander.IsExpanded;
    });
}

