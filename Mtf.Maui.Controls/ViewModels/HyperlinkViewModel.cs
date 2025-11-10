using CommunityToolkit.Mvvm.ComponentModel;

namespace Mtf.Maui.Controls.ViewModels;

public partial class HyperlinkViewModel : ObservableObject
{
    private string url = String.Empty;
    private string linkLabel = String.Empty;

    public string Url
    {
        get => url;
        set => SetProperty(ref url, value);
    }

    public string LinkLabel
    {
        get => linkLabel;
        set => SetProperty(ref linkLabel, value);
    }
}