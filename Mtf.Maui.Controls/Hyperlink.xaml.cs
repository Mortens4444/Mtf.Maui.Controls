using Mtf.Maui.Controls.Extensions;
using Mtf.Maui.Controls.Services;
using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class Hyperlink : ContentView
{
    private int isNavigating;

    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(Hyperlink),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<Hyperlink, HyperlinkViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.Url = value);
            });

    public static readonly BindableProperty LinkLabelProperty =
        BindableProperty.Create(nameof(LinkLabel), typeof(string), typeof(Hyperlink),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<Hyperlink, HyperlinkViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.LinkLabel = value);
            });

    public Hyperlink()
    {
        InitializeComponent();

        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                Task.Run(() => Hyperlink_RequestNavigateAsync(Url));
            })
        });
        GestureRecognizers.Add(new PointerGestureRecognizer
        {
            PointerEnteredCommand = new Command(OnPointerEntered),
            PointerExitedCommand = new Command(OnPointerExited)
        });
    }

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

    private void OnPointerEntered() => HyperlinkLabel.TextColor = Colors.Blue;

    private void OnPointerExited() => HyperlinkLabel.TextColor = Colors.LightBlue;

    private async Task Hyperlink_RequestNavigateAsync(string uri)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(uri) || Interlocked.Exchange(ref isNavigating, 1) == 1)
            {
                return;
            }

            if (Uri.TryCreate(uri, UriKind.Absolute, out var uriResult))
            {
                _ = await Launcher.OpenAsync(uriResult).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            await ex.ShowErrorAsync().ConfigureAwait(false);
        }
        finally
        {
            Interlocked.Exchange(ref isNavigating, 0);
        }
    }
}