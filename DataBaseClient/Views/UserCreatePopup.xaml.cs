using DataBaseClient.Models;

namespace DataBaseClient.Views;

public partial class UserCreatePopup : CommunityToolkit.Maui.Views.Popup
{
	public UserCreatePopup()
	{
		InitializeComponent();
        ResultWhenUserTapsOutsideOfPopup = null;
    }

    void OnYesButtonClicked(object sender, EventArgs e) => Close(new User
    {
        Login = loginEntry.Text,
        Email = emailEntry.Text,
        PhoneNumber = phoneEntry.Text
    });

    void OnNoButtonClicked(object sender, EventArgs e) => Close(null);
}