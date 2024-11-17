using System.ComponentModel;

namespace Mtf.Maui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Hyperlink : ContentView, INotifyPropertyChanged
{
    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(Hyperlink));

    public static readonly BindableProperty LinkLabelProperty =
        BindableProperty.Create(nameof(LinkLabel), typeof(string), typeof(Hyperlink));

    public string Url
    {
        get => (string)GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }

    public string LinkLabel
    {
        get => (string)GetValue(LinkLabelProperty);
        set => SetValue(LinkLabelProperty, value);
    }

    public Hyperlink()
    {
        InitializeComponent();

        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => Hyperlink_RequestNavigate(this, Url))
        });
        GestureRecognizers.Add(new PointerGestureRecognizer
        {
            PointerEnteredCommand = new Command(OnPointerEntered),
            PointerExitedCommand = new Command(OnPointerExited)
        });
    }

    private void OnPointerEntered() => HyperlinkLabel.TextColor = Colors.Blue;

    private void OnPointerExited() => HyperlinkLabel.TextColor = Colors.LightBlue;

    private static async void Hyperlink_RequestNavigate(object _, string uri)
    {
        if (Uri.TryCreate(uri, UriKind.Absolute, out var uriResult))
        {
            _ = await Launcher.OpenAsync(uriResult).ConfigureAwait(false);
        }
    }
}