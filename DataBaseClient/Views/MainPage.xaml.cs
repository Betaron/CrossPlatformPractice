using DataBaseClient.ViewModels;

namespace DataBaseClient;

public partial class MainPage : ContentPage
{
    public MainPage(UsersViewModel usersViewModel)
	{
		InitializeComponent();

		BindingContext = usersViewModel;
    }
}
