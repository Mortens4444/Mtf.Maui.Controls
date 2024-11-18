using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Models;

namespace Mtf.Maui.Controls.ViewModels;

public partial class EntryWithLabelViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCopyToClipboard))]
    private string text = String.Empty;

    [ObservableProperty]
    private string label = String.Empty;

    [ObservableProperty]
    private string placeholder = String.Empty;

    [ObservableProperty]
    private bool isPassword;

    [ObservableProperty]
    private bool isReadOnly;

    [ObservableProperty]
    private bool isLabelVisible;

    [ObservableProperty]
    private Keyboard keyboard = Keyboard.Default;

    [ObservableProperty]
    private Color entryTextColor = Colors.White;

    [ObservableProperty]
    private LayoutOptions entryHorizontalOptions = LayoutOptions.Start;

    [ObservableProperty]
    private LayoutOptions entryVerticalOptions = LayoutOptions.Start;

    [ObservableProperty]
    private int entryMinimumWidthRequest = 50;

    [ObservableProperty]
    private int entryMinimumHeightRequest = 20;

    public bool CanCopyToClipboard => !String.IsNullOrWhiteSpace(Text);

    [RelayCommand(CanExecute = nameof(CanCopyToClipboard))]
    private async Task CopyToClipboard()
    {
        if (!String.IsNullOrWhiteSpace(Text))
        {
            await Clipboard.SetTextAsync(Text).ConfigureAwait(false);
            _ = WeakReferenceMessenger.Default.Send(new ShowInfoMessage("Success", "Text copied to clipboard."));
        }
    }

    public void ValidateText(Func<string, bool> validationLogic, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(validationLogic);

        if (!validationLogic(Text))
        {
            _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(errorMessage));
        }
    }

    public event EventHandler<TextChangedEventArgs>? TextChanged;

    public void OnTextChanged(object sender, TextChangedEventArgs e) => TextChanged?.Invoke(this, e);
}
