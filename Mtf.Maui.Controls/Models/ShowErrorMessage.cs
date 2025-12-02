using CommunityToolkit.Mvvm.Messaging.Messages;
using Mtf.Maui.Controls.Extensions;

namespace Mtf.Maui.Controls.Models;

public class ShowErrorMessage : ValueChangedMessage<string>
{
    public Exception? Exception { get; }

    public ShowErrorMessage(Exception exception)
#if DEBUG
        : base(exception?.GetDetails() ?? throw new ArgumentNullException(nameof(exception)))
#else
        : base(exception?.Message ?? throw new ArgumentNullException(nameof(exception)))
#endif
    {
        Exception = exception;
    }

    public ShowErrorMessage(string title, Exception exception)
        : base(exception.GetDetails(title))
    {
        Exception = exception;
    }

    public ShowErrorMessage(string message)
        : base(message)
    { }
}