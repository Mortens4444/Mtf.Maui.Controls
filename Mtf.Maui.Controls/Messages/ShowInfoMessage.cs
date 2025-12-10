using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Mtf.Maui.Controls.Messages;

public class ShowInfoMessage(string title, string message) : ValueChangedMessage<string>(string.Empty)
{
    public string Title { get; init; } = title;

    public string Message { get; init; } = message;
}