using Mtf.Maui.Controls.Interfaces;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class LabelWithToggleCommand : ContentView, IHaveText
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LabelWithToggleCommand));

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LabelWithToggleCommand));

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelWithToggleCommand), String.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty) ?? this;
        set => SetValue(CommandParameterProperty, value);
    }

    public LabelWithToggleCommand()
    {
        InitializeComponent();
        textLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
    }

    public override string ToString() => Text;
}