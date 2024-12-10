using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class ToggleButtonView : ContentView
{
    public static readonly BindableProperty ImageNameProperty =
        BindableProperty.Create(nameof(ImageName), typeof(string), typeof(ToggleButtonView));

    public static readonly BindableProperty VisualElementProperty =
        BindableProperty.Create(nameof(VisualElement), typeof(VisualElement), typeof(ToggleButtonView));

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ToggleButtonView), String.Empty);

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string ImageName
    {
        get => (string)GetValue(ImageNameProperty);
        set => SetValue(ImageNameProperty, value);
    }

    public VisualElement VisualElement
    {
        get => (VisualElement)GetValue(VisualElementProperty);
        set => SetValue(VisualElementProperty, value);
    }

    public ICommand ToggleVisibilityCommand { get; set; }

    public ToggleButtonView()
    {
        InitializeComponent();
        ToggleVisibilityCommand = new Command(
            () =>
            {
                var descendants = (VisualElement?.Parent?.GetVisualTreeDescendants() ?? []).Where(element => element is ContentView and
                    not ToggleButtonView and
                    not UriOpenerButtonWithLabelView);
                foreach (var descendant in descendants)
                {
                    if (descendant is VisualElement visualElement)
                    {
                        visualElement.IsVisible = false;
                    }
                }
                if (VisualElement != null)
                {
                    VisualElement.IsVisible = true;
                }
            });
        BindingContext = this;
    }
}
