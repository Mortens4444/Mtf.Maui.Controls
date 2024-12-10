namespace Mtf.Maui.Controls;

public partial class CheckBoxWithLabel : ContentView
{
    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(CheckBoxWithLabel), String.Empty);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBoxWithLabel), Colors.White);

    public static readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxWithLabel), false, BindingMode.TwoWay);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public Command ToggleCommand => new(() =>
    {
        checkBox.IsChecked = !checkBox.IsChecked;
    });

    public CheckBoxWithLabel() => InitializeComponent();
}