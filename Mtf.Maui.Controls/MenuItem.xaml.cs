using Mtf.Maui.Controls.ViewModels;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class MenuItem : ContentView
{
    private readonly MenuItemViewModel viewModel = new();

    public static readonly BindableProperty ParameterProperty =
        BindableProperty.Create(nameof(Parameter), typeof(object), typeof(MenuItem), default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItem)bindable;
                view.viewModel.Parameter = newValue;
            });

    public static readonly BindableProperty AfterExecutionProperty =
        BindableProperty.Create(nameof(AfterExecution), typeof(ICommand), typeof(MenuItem), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItem)bindable;
                view.viewModel.AfterExecution = newValue as ICommand;
            });

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(List<string>), typeof(MenuItem), new List<string> { MenuItemViewModel.Unknown },
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItem)bindable;
                view.viewModel.ImageSource = newValue as List<string> ?? new List<string> { MenuItemViewModel.Unknown };
            });

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MenuItem), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItem)bindable;
                view.viewModel.LabelText = newValue as string ?? String.Empty;
            });

    public static readonly BindableProperty PageTypeProperty =
        BindableProperty.Create(nameof(PageType), typeof(Type), typeof(MenuItem), typeof(Page),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItem)bindable;
                view.viewModel.PageType = newValue as Type ?? typeof(Page);
            });

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

    public Type PageType
    {
        get => (Type)GetValue(PageTypeProperty);
        set => SetValue(PageTypeProperty, value);
    }

    public object Parameter
    {
        get => GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }

    public ICommand AfterExecution
    {
        get => (ICommand)GetValue(AfterExecutionProperty);
        set => SetValue(AfterExecutionProperty, value);
    }

    public MenuItemViewModel ViewModel => viewModel;

    public MenuItem()
    {
        InitializeComponent();
        InnerRoot.BindingContext = viewModel;
    }
}