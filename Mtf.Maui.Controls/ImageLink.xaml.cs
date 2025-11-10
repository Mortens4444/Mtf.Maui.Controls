using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class ImageLink : ContentView
{
    private readonly ImageLinkViewModel viewModel = new();
    
    public static readonly BindableProperty ParameterProperty =
        BindableProperty.Create(nameof(Parameter), typeof(object), typeof(ImageLink), default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (ImageLink)bindable;
                view.viewModel.Parameter = newValue;
            });

    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(ImageLink), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (ImageLink)bindable;
                view.viewModel.Url = newValue as string ?? String.Empty;
            });

    public ImageLink()
    {
        InitializeComponent();
        InnerRoot.BindingContext = viewModel;
    }

    public string Url
    {
        get => (string)GetValue(UrlProperty);
        set => SetValue(UrlProperty, value ?? String.Empty);
    }

    public object Parameter
    {
        get => GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }
}