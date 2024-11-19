using Mtf.Maui.Controls.Services;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class UriOpenerButtonWithLabelView : ContentView
{
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(List<string>), typeof(UriOpenerButtonWithLabelView), new List<string> { "unknown" });

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(UriOpenerButtonWithLabelView), String.Empty);

    public static readonly BindableProperty UrlToOpenProperty =
        BindableProperty.Create(nameof(UrlToOpen), typeof(string), typeof(UriOpenerButtonWithLabelView), String.Empty);

    public UriOpenerButtonWithLabelView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public string ImageName
    {
        get => ((List<string>)GetValue(ImageSourceProperty))[0];
        set => SetValue(ImageSourceProperty, new List<string> { value });
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string UrlToOpen
    {
        get => (string)GetValue(UrlToOpenProperty);
        set => SetValue(UrlToOpenProperty, value);
    }

    public List<string> ImageSource => [ImageName];

    public ICommand OpenUriCommand => new Command<string>(UriOpener.OpenUri);
}