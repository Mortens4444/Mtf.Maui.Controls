using Mtf.Maui.Controls.Services;
using Mtf.Maui.Controls.ViewModels;

namespace Mtf.Maui.Controls;

public partial class CheckBoxWithLabel : ContentView
{
    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(CheckBoxWithLabel), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<CheckBoxWithLabel, CheckBoxWithLabelViewModel, string>
                    (bindable, newValue as string ?? String.Empty, (vm, value) => vm.Label = value);
            });

    public static readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxWithLabel), false, BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                PropertyChanger.OnPropertyChanged<CheckBoxWithLabel, CheckBoxWithLabelViewModel, bool>
                    (bindable, (bool)newValue, (vm, value) => vm.IsChecked = value);
            });

    public CheckBoxWithLabel() => InitializeComponent();

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public Command ToggleCommand => new(() =>
    {
        // Works only with this hack, dunno why...
        checkBox.IsChecked = !checkBox.IsChecked;
    });
}
