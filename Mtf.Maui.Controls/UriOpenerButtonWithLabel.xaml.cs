using Mtf.Maui.Controls.Services;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class UriOpenerButtonWithLabel : ContentView
{
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(List<string>), typeof(UriOpenerButtonWithLabel), new List<string> { "unknown" });

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(UriOpenerButtonWithLabel), String.Empty);

    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(UriOpenerButtonWithLabel), String.Empty);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBoxWithLabel), Colors.White);

    public static readonly BindableProperty LabelHorizontalOptionsProperty =
        BindableProperty.Create(nameof(LabelHorizontalOptions), typeof(LayoutOptions), typeof(CheckBoxWithLabel), LayoutOptions.Center);

    public UriOpenerButtonWithLabel()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public LayoutOptions LabelHorizontalOptions
    {
        get => (LayoutOptions)GetValue(LabelHorizontalOptionsProperty);
        set => SetValue(LabelHorizontalOptionsProperty, value);
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

    public string Url
    {
        get => (string)GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }

    public List<string> ImageSource => [ImageName];

    public ICommand OpenUriCommand => new Command<string>((uri => _ = UriOpener.OpenUriAsync(new Uri(uri))));
}