using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseClient.Exceptions;

namespace DataBaseClient.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    protected async void WrapInExceptionHandler(Action action)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            action.Invoke();
        }
        catch (ValidationException ex)
        {
            await Application.Current.MainPage.DisplayAlert("Warning!", ex.ValidationMessage, "Ok");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
