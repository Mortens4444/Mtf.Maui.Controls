using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Messages;
using System.Windows.Input;

namespace Mtf.Maui.Controls.ViewModels;

public partial class ImageLinkViewModel : ObservableObject
{
    private int isNavigating;
    private string url;
    private object? parameter;
    private ICommand? afterExecution;

    public const string Unknown = "unknown.scale-100";

    public List<string> imageSource = new(new List<string> { Unknown });

    public List<string> ImageSource
    {
        get => imageSource;
        set => SetProperty(ref imageSource, value);
    }
    public string Url
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
                    _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(ex));
                    //Debug.WriteLine(ex);
                    //await Console.Error.WriteLineAsync(ex.ToString()).ConfigureAwait(false);
                }
            }
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
