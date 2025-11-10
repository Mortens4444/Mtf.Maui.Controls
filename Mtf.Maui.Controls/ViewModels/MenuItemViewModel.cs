using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using Mtf.Maui.Controls.Services;
using System.Windows.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class MenuItemViewModel : ObservableObject
{
    private int isNavigating;
    private Color textColor = Colors.White;
    private string labelText = String.Empty;
    private object? parameter;
    private ICommand? afterExecution;
    private LayoutOptions labelHorizontalOptions = LayoutOptions.Center;
    private Type pageType = typeof(Page);

    public const string Unknown = "unknown.scale-100";

    public List<string> imageSource = new(new List<string> { Unknown });

    public List<string> ImageSource
    {
        get => imageSource;
        set => SetProperty(ref imageSource, value);
    }
    public Color TextColor
    {
        get => textColor;
        set => SetProperty(ref textColor, value);
    }

    public LayoutOptions LabelHorizontalOptions
    {
        get => labelHorizontalOptions;
        set => SetProperty(ref labelHorizontalOptions, value);
    }

    public string LabelText
    {
        get => labelText;
        set => SetProperty(ref labelText, value);
    }

    public Type PageType
    {
        get => pageType;
        set => SetProperty(ref pageType, value);
    }

    public object? Parameter
    {
        get => parameter;
        set => SetProperty(ref parameter, value);
    }

    public ICommand? AfterExecution
    {
        get => afterExecution;
        set => SetProperty(ref afterExecution, value);
    }

    public ICommand NavigateCommand => new AsyncRelayCommand(NavigateToPageAsync);

    private async Task NavigateToPageAsync()
    {
        try
        {
            if (Interlocked.Exchange(ref isNavigating, 1) == 1)
            {
                return;
            }

            var page = await NavigationService.NavigateToPageAsync(PageType, Parameter).ConfigureAwait(false);

            void disappearingHandler(object? sender, EventArgs e)
            {
                page.Disappearing -= disappearingHandler;
                AfterExecution?.Execute(null);
            }

            page.Disappearing += disappearingHandler;
        }
        catch (Exception ex)
        {
            _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(ex));
        }
        finally
        {
            Interlocked.Exchange(ref isNavigating, 0);
        }
    }
}
