using CommunityToolkit.Mvvm.Messaging.Messages;
using Mtf.Maui.Controls.Extensions;

namespace Mtf.Maui.Controls.Messages;

public class ShowErrorMessage : ValueChangedMessage<string>
{
    public bool ShowInnerException { get; }

    public Exception? Exception { get; }

    public ShowErrorMessage(Exception exception, bool showInnerException = true)
#if DEBUG
        : base(exception?.GetDetails(exception.GetType().Name, showInnerException) ?? throw new ArgumentNullException(nameof(exception)))
#else
        : base(exception?.Message ?? throw new ArgumentNullException(nameof(exception)))
#endif
    {
        Exception = exception;
        ShowInnerException = showInnerException;
    }

    public ShowErrorMessage(string title, Exception exception, bool showInnerException = true)
        : base(exception.GetDetails(title, showInnerException))
    {
        Exception = exception;
        ShowInnerException = showInnerException;
    }

    public ShowErrorMessage(string message)
        : base(message)
    { }
}