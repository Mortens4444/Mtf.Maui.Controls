namespace Mtf.Maui.Controls;

public partial class LabelWithTitle : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelWithTitle), String.Empty, propertyChanged: OnTextOrTitleChanged);

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(LabelWithTitle), String.Empty, propertyChanged: OnTextOrTitleChanged);

    public static readonly BindableProperty TextStyleProperty =
        BindableProperty.Create(nameof(TextStyle), typeof(Style), typeof(LabelWithTitle), null, propertyChanged: OnTitleStyleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Style TextStyle
    {
        get => (Style)GetValue(TextStyleProperty);
        set => SetValue(TextStyleProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public LabelWithTitle()
    {
        InitializeComponent();
        titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        textLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
    }

    private static void OnTitleStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var labelWithTitle = (LabelWithTitle)bindable;
        labelWithTitle.textLabel.Style = (Style)newValue;
    }

    private static void OnTextOrTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var labelWithTitle = (LabelWithTitle)bindable;
        labelWithTitle.UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        titleLabel.IsVisible = !String.IsNullOrWhiteSpace(Title) && !String.IsNullOrWhiteSpace(Text);
        textLabel.IsVisible = !String.IsNullOrWhiteSpace(Text);
        stackLayout.IsVisible = textLabel.IsVisible;
    }
}