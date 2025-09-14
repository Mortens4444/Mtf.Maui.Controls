using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;

namespace Mtf.Maui.Controls;

public partial class EntryWithLabel : ContentView
{
    public static readonly BindableProperty LabelTextColorProperty =
        BindableProperty.Create(nameof(LabelTextColor), typeof(Color), typeof(EntryWithLabel), Colors.White);
 
    public static readonly BindableProperty EntryTextColorProperty =
        BindableProperty.Create(nameof(EntryTextColor), typeof(Color), typeof(EntryWithLabel), Colors.White);

    public static readonly BindableProperty EntryMinimumWidthRequestProperty =
        BindableProperty.Create(nameof(EntryMinimumWidthRequestProperty), typeof(int), typeof(EntryWithLabel), 50);

    public static readonly BindableProperty EntryMinimumHeightRequestProperty =
        BindableProperty.Create(nameof(EntryMinimumHeightRequest), typeof(int), typeof(EntryWithLabel), 20);

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(EntryWithLabel), String.Empty);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryWithLabel), String.Empty);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithLabel), String.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryWithLabel), Keyboard.Default);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryWithLabel), false);

    public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(EntryWithLabel), false);

    public static readonly BindableProperty IsLabelVisibleProperty =
        BindableProperty.Create(nameof(IsLabelVisible), typeof(bool), typeof(EntryWithLabel), true);

    public static readonly BindableProperty EntryHorizontalOptionsProperty =
        BindableProperty.Create(nameof(EntryHorizontalOptions), typeof(LayoutOptions), typeof(EntryWithLabel), LayoutOptions.Start);

    public static readonly BindableProperty EntryVerticalOptionsProperty =
        BindableProperty.Create(nameof(EntryVerticalOptions), typeof(LayoutOptions), typeof(EntryWithLabel), LayoutOptions.Start);

    public event EventHandler<TextChangedEventArgs>? TextChanged;
    
    public Color LabelTextColor
    {
        get => (Color)GetValue(LabelTextColorProperty);
        set => SetValue(LabelTextColorProperty, value);
    }

    public Color EntryTextColor
    {
        get => (Color)GetValue(EntryTextColorProperty);
        set => SetValue(EntryTextColorProperty, value);
    }

    public int EntryMinimumWidthRequest
    {
        get => (int)GetValue(EntryMinimumWidthRequestProperty);
        set => SetValue(EntryMinimumWidthRequestProperty, value);
    }

    public int EntryMinimumHeightRequest
    {
        get => (int)GetValue(EntryMinimumHeightRequestProperty);
        set => SetValue(EntryMinimumHeightRequestProperty, value);
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Placeholder
    {
        get
        {
            var result = (string)GetValue(PlaceholderProperty);
            return String.IsNullOrWhiteSpace(result) ? Label : result;
        }
        set => SetValue(PlaceholderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public bool IsLabelVisible
    {
        get => (bool)GetValue(IsLabelVisibleProperty);
        set => SetValue(IsLabelVisibleProperty, value);
    }

    public LayoutOptions EntryHorizontalOptions
    {
        get => (LayoutOptions)GetValue(EntryHorizontalOptionsProperty);
        set => SetValue(EntryHorizontalOptionsProperty, value);
    }

    public LayoutOptions EntryVerticalOptions
    {
        get => (LayoutOptions)GetValue(EntryVerticalOptionsProperty);
        set => SetValue(EntryVerticalOptionsProperty, value);
    }

    public EntryWithLabel() => InitializeComponent();

    [RelayCommand]
    private static async Task CopyToClipboard(string text)
    {
        if (!String.IsNullOrWhiteSpace(text))
        {
            await Clipboard.SetTextAsync(text).ConfigureAwait(false);
            _ = WeakReferenceMessenger.Default.Send(new ShowInfoMessage("Success", "Text copied to clipboard."));
        }
    }

    protected virtual void OnTextChanged(object sender, TextChangedEventArgs e) => TextChanged?.Invoke(this, e);
}