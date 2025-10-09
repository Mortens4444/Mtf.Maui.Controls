using Mtf.Maui.Controls.Services;
using Mtf.Maui.Controls.ViewModels;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class MenuItemView : ContentView
{
    private readonly MenuItemViewModel viewModel = new MenuItemViewModel();

    public static readonly BindableProperty ParameterProperty =
        BindableProperty.Create(nameof(Parameter), typeof(object), typeof(MenuItemView), default(object),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItemView)bindable;
                view.viewModel.Parameter = newValue;
            });

    public static readonly BindableProperty AfterExecutionProperty =
        BindableProperty.Create(nameof(AfterExecution), typeof(ICommand), typeof(MenuItemView), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItemView)bindable;
                view.viewModel.AfterExecution = newValue as ICommand;
            });

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(List<string>), typeof(MenuItemView), new List<string> { MenuItemViewModel.Unknown },
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItemView)bindable;
                view.viewModel.ImageSource = newValue as List<string> ?? new List<string> { MenuItemViewModel.Unknown };
            });

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MenuItemView), String.Empty,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItemView)bindable;
                view.viewModel.LabelText = newValue as string ?? String.Empty;
            });

    public static readonly BindableProperty PageTypeProperty =
        BindableProperty.Create(nameof(PageType), typeof(Type), typeof(MenuItemView), typeof(Page),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (MenuItemView)bindable;
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

    public MenuItemView()
    {
        InitializeComponent();
        InnerRoot.BindingContext = viewModel;
    }
}