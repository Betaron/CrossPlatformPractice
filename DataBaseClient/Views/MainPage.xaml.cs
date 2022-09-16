using DataBaseClient.ViewModels;

namespace DataBaseClient.Views;

public partial class MainPage : ContentPage
{
    public MainPage(UsersViewModel usersViewModel)
	{
		InitializeComponent();

		BindingContext = usersViewModel;
    }
}
