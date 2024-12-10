using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using Mtf.Maui.Controls.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class MenuItemViewModel : ObservableObject
{
    private bool isNavigating;
    public const string Unknown = "unknown.scale-100";

    [ObservableProperty]
    private List<string> imageSource = new() { Unknown };

    [ObservableProperty]
    private Color textColor = Colors.White;

    [ObservableProperty]
    private LayoutOptions labelHorizontalOptions = LayoutOptions.Center;

    [ObservableProperty]
    private string labelText = String.Empty;

    [ObservableProperty]
    private Type pageType = typeof(Page);

    [ObservableProperty]
    private object? parameter;

    [ObservableProperty]
    private ICommand? afterExecution;

    public ICommand NavigateCommand => new AsyncRelayCommand(NavigateToPageAsync);

    private async Task NavigateToPageAsync()
    {
        try
        {
            if (isNavigating)
            {
                return;
            }
            isNavigating = true;
            Debug.WriteLine($"NavigateToPageAsync {PageType.Name}");
            Debug.WriteLine($"NavigateToPageAsync {Parameter}");
            var page = await NavigationService.NavigateToPageAsync(PageType, Parameter).ConfigureAwait(false);
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
        }
    }
}
