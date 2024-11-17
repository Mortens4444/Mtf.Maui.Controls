using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using Mtf.Maui.Controls.Services;
using System.Windows.Input;

namespace Mtf.Maui.Controls;

public partial class MenuItemView : ContentView
{
    private bool isNavigating;

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(List<string>), typeof(MenuItemView), new List<string> { "unknown" });

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MenuItemView), String.Empty);

    public static readonly BindableProperty PageTypeProperty =
        BindableProperty.Create(nameof(PageType), typeof(Type), typeof(MenuItemView), typeof(Page));

    public static readonly BindableProperty ParameterProperty =
        BindableProperty.Create(nameof(Parameter), typeof(object), typeof(MenuItemView), default);

    public static readonly BindableProperty AfterExecutionProperty =
        BindableProperty.Create(nameof(AfterExecution), typeof(ICommand), typeof(MenuItemView), default(ICommand));

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

    public List<string> ImageSource => [ImageName];

    public ICommand NavigateCommand => new Command(async () => await NavigateToPageAsync().ConfigureAwait(true));

    public MenuItemView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async Task NavigateToPageAsync()
    {
        try
        {
            if (isNavigating)
            {
                return;
            }
            isNavigating = true;
            IsEnabled = false;

            var page = await NavigationService.NavigateToPageAsync(PageType, Parameter).ConfigureAwait(true);
            page.Disappearing += OnDisappearing;

            void OnDisappearing(object? sender, EventArgs e)
            {
                page.Disappearing -= OnDisappearing;
                AfterExecution?.Execute(null);
            }
        }
        catch (Exception ex)
        {
            _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(ex));
        }
        finally
        {
            isNavigating = false;
            IsEnabled = true;
        }
    }
}