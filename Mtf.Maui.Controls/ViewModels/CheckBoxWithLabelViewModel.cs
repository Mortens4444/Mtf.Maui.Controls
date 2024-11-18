using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class CheckBoxWithLabelViewModel : ObservableObject
{
    [ObservableProperty]
    private string label = String.Empty;

    [ObservableProperty]
    private bool isChecked;

    [ObservableProperty]
    private bool isEnabled;

    [RelayCommand]
    private void Toggle() => IsChecked = !IsChecked;
}