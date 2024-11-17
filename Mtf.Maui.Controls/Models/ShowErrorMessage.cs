using CommunityToolkit.Mvvm.Messaging.Messages;
using Mtf.Maui.Controls.Extensions;

namespace Mtf.Maui.Controls.Models;

public class ShowErrorMessage : ValueChangedMessage<string>
{
    public ShowErrorMessage(Exception exception)
#if DEBUG
        : base(exception.GetDetails())
#else
        : base(exception.Message)
#endif
    { }

    public ShowErrorMessage(string title, Exception exception)
        : base(exception.GetDetails(title))
    { }

    public ShowErrorMessage(string message)
        : base(message)
    { }
}