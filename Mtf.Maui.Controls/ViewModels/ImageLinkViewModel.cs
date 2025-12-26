using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mtf.Maui.Controls.Extensions;
using System.Windows.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class ImageLinkViewModel : ObservableObject
{
    private int isNavigating;
    private string? url;
    private object? parameter;
    private ICommand? afterExecution;

    public const string Unknown = "unknown.scale-100";

    private List<string> imageSource = new(new List<string> { Unknown });

    public List<string> ImageSource
    {
        get => imageSource;
        set => SetProperty(ref imageSource, value);
    }

    public string? Url
    {
        get => url;
        set => SetProperty(ref url, value);
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

    public ICommand NavigateCommand => new AsyncRelayCommand(NavigateToUrl);

    private async Task NavigateToUrl()
    {
        try
        {
            if (String.IsNullOrWhiteSpace(Url) || Interlocked.Exchange(ref isNavigating, 1) == 1)
            {
                return;
            }

            if (Uri.TryCreate(Url, UriKind.Absolute, out var uriResult))
            {
                try
                {
                    _ = await Launcher.OpenAsync(uriResult).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await ex.ShowErrorAsync().ConfigureAwait(false);
                }
            }
        }
        catch (Exception ex)
        {
            await ex.ShowErrorAsync().ConfigureAwait(false);
        }
        finally
        {
            Interlocked.Exchange(ref isNavigating, 0);
        }
    }
}
