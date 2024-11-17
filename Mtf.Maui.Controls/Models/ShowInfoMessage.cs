using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Mtf.Maui.Controls.Models;

public class ShowInfoMessage(string title, string message) : ValueChangedMessage<string>(String.Empty)
{
    public string Title { get; init; } = title;

    public string Message { get; init; } = message;
}