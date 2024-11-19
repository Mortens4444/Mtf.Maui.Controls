using Mtf.Maui.Controls.Services;
using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class EntryWithLabel : ContentView
{
    public static readonly BindableProperty EntryTextColorProperty =
        BindableProperty.Create(nameof(EntryTextColor), typeof(Color), typeof(EntryWithLabel), Colors.White,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, Color>
                    (bindable, newValue as Color ?? Colors.White, (vm, value) => vm.EntryTextColor = value);
            });

    public static readonly BindableProperty EntryMinimumWidthRequestProperty =
        BindableProperty.Create(nameof(EntryMinimumWidthRequest), typeof(int), typeof(EntryWithLabel), 50,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, int>
                    (bindable, (int)newValue, (vm, value) => vm.EntryMinimumWidthRequest = value);
            });

    public static readonly BindableProperty EntryMinimumHeightRequestProperty =
        BindableProperty.Create(nameof(EntryMinimumHeightRequest), typeof(int), typeof(EntryWithLabel), 20,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, int>
                    (bindable, (int)newValue, (vm, value) => vm.EntryMinimumHeightRequest = value);
            });

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(EntryWithLabel), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.Label = value);
            });

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryWithLabel), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.Placeholder = value);
            });

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithLabel), String.Empty, BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.Text = value);
            });

    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryWithLabel), Keyboard.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, Keyboard>
                    (bindable, newValue as Keyboard ?? Keyboard.Default, (vm, value) => vm.Keyboard = value);
            });

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryWithLabel), false,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, bool>
                    (bindable, (bool)newValue, (vm, value) => vm.IsPassword = value);
            });

    public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(EntryWithLabel), false,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, bool>
                    (bindable, (bool)newValue, (vm, value) => vm.IsReadOnly = value);
            });

    public static readonly BindableProperty IsLabelVisibleProperty =
        BindableProperty.Create(nameof(IsLabelVisible), typeof(bool), typeof(EntryWithLabel), true,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, bool>
                    (bindable, (bool)newValue, (vm, value) => vm.IsLabelVisible = value);
            });

    public static readonly BindableProperty EntryHorizontalOptionsProperty =
        BindableProperty.Create(nameof(EntryHorizontalOptions), typeof(LayoutOptions), typeof(EntryWithLabel), LayoutOptions.Start,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, LayoutOptions>
                    (bindable, (LayoutOptions)newValue, (vm, value) => vm.EntryHorizontalOptions = value);
            });

    public static readonly BindableProperty EntryVerticalOptionsProperty =
        BindableProperty.Create(nameof(EntryVerticalOptions), typeof(LayoutOptions), typeof(EntryWithLabel), LayoutOptions.Start,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<EntryWithLabel, EntryWithLabelViewModel, LayoutOptions>
                    (bindable, (LayoutOptions)newValue, (vm, value) => vm.EntryVerticalOptions = value);
            });

    public EntryWithLabel() => InitializeComponent();

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
        get => (string)GetValue(PlaceholderProperty);
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

    public event EventHandler<TextChangedEventArgs>? TextChanged;

    private void OnTextChanged(object sender, TextChangedEventArgs e) => TextChanged?.Invoke(this, e);
}