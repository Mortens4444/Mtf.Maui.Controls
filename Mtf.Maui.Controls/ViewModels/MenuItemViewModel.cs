using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;
using Mtf.Maui.Controls.Services;
using System.Windows.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class MenuItemViewModel : ObservableObject
{
    private bool isNavigating;

    [ObservableProperty]
    private List<string> imageSource = new() { "unknown" };

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
