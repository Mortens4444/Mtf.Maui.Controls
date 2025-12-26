using CommunityToolkit.Mvvm.Messaging;
using Mtf.Maui.Controls.Messages;
using System.Diagnostics;
using System.Text;

namespace Mtf.Maui.Controls.Extensions;

public static class ExceptionExtensions
{
    public static Task ShowError(this Exception ex)
    {
        return MainThread.InvokeOnMainThreadAsync(() =>
        {
            var details = ex.GetDetails();
            Debug.Write(details);
            Console.WriteLine(details);
            _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(ex));
        });
    }

    public static Task ShowErrorAsync(this Exception ex, Page? mainPage = null)
    {
        return MainThread.InvokeOnMainThreadAsync(async () =>
        {
            var details = ex.GetDetails();
            Debug.Write(details);
            Console.WriteLine(details);

            if (mainPage != null)
            {
                await mainPage.DisplayAlert("Unhandled Exception", ex.GetLastInnerExceptionMessage(), "OK").ConfigureAwait(true);
            }
            else
            {
                _ = WeakReferenceMessenger.Default.Send(new ShowErrorMessage(ex));
            }
        });
    }

    public static string GetDetails(this Exception exception)
    {
        return exception == null ?
            throw new ArgumentNullException(nameof(exception)) :
            GetDetails(exception, exception.GetType().Name);
    }

    public static string GetDetails(this Exception exception, string title)
    {
        var result = new StringBuilder();
        var ex = exception;
        var i = 1;
        while (ex != null)
        {
            var exceptionDetails = GetExceptionDetails($"{title} {i++}", ex);
            _ = result.AppendLine(exceptionDetails);
            ex = ex.InnerException;
        }

        return result.ToString();
    }

    public static string GetLastInnerExceptionMessage(this Exception exception)
    {
        var result = String.Empty;
        var ex = exception;
        while (ex != null)
        {
            result = ex.Message;
            ex = ex.InnerException;
        }
        return result;
    }

    private static string GetExceptionDetails(string title, Exception exception)
    {
        return $"-------------------------- {title} ----------------------------{Environment.NewLine}" +
            $"Type: {exception.GetType()}{Environment.NewLine}" +
            $"Message: {exception.Message}{Environment.NewLine}" +
            $"StackTrace: {exception.StackTrace}{Environment.NewLine}" +
            $"------------------------- {title} End --------------------------{Environment.NewLine}";
    }
}
