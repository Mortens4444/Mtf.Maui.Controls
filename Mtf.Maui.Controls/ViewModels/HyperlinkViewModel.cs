using CommunityToolkit.Mvvm.ComponentModel;

namespace Mtf.Maui.Controls.ViewModels;

public partial class HyperlinkViewModel : ObservableObject
{
    [ObservableProperty]
    private string url = String.Empty;

    [ObservableProperty]
    private string linkLabel = String.Empty;
}