using CommunityToolkit.Mvvm.ComponentModel;
using Mtf.Maui.Controls.Interfaces;
using System.Windows.Input;

namespace Mtf.Maui.Controls.Test.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public double PriceMultiplier { get; set; }

    public ICommand ToggleTextCommand { get; } = new Command((sender) =>
    {
        var labelWithToggleCommand = sender as IHaveText;
        labelWithToggleCommand.Text = "Changed...";
        //labelWithToggleCommand.Text = labelWithToggleCommand.Text.ChangeExpanderText();

        //var expander = (sender as ContentView)?.Parent?.Parent as Expander;
        //expander.IsExpanded = !expander.IsExpanded;
    });
}
