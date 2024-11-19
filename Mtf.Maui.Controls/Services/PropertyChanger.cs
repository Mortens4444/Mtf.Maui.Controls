namespace Mtf.Maui.Controls.Services;

public static class PropertyChanger
{
    public static void OnPropertyChanged<TView, TViewModel, TValue>(
        object bindable,
        TValue newValue,
        Action<TViewModel, TValue> updateAction)
        where TView : BindableObject
        where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(updateAction);
        if (bindable is TView view && view.BindingContext is TViewModel viewModel)
        {
            updateAction(viewModel, newValue);
        }
    }
}
