using CommunityToolkit.Mvvm.ComponentModel;
using Mtf.Maui.Controls.Interfaces;
using System.Windows.Input;

namespace Mtf.Maui.Controls.Test.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public double PriceMultiplier { get; set; } = 1.0;

    private double blockSize = 8;
    public double BlockSize
    {
        get => blockSize;
        set => SetProperty(ref blockSize, value);
    }

    private double minBlockSize = 8;
    public double MinBlockSize
    {
        get => minBlockSize;
        set => SetProperty(ref minBlockSize, value);
    }

    private double maxBlockSize = 800;
    public double MaxBlockSize
    {
        get => maxBlockSize;
        set => SetProperty(ref maxBlockSize, value);
    }

    public ICommand ToggleTextCommand { get; }
    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }

    public MainViewModel()
    {
        ToggleTextCommand = new Command(sender =>
        {
            if (sender is IHaveText label)
            {
                label.Text = "Changed...";
                //labelWithToggleCommand.Text = labelWithToggleCommand.Text.ChangeExpanderText();

                //var expander = (sender as ContentView)?.Parent?.Parent as Expander;
                //expander.IsExpanded = !expander.IsExpanded;
            }
        });

        IncrementCommand = new Command(() =>
        {
            BlockSize += 8;
            //MinBlockSize = 80000;
            OnPropertyChanged(nameof(MinBlockSize));
        });

        DecrementCommand = new Command(() =>
        {
            BlockSize -= 8;
            //MaxBlockSize = 4;
            OnPropertyChanged(nameof(MaxBlockSize));
        });
    }
}
