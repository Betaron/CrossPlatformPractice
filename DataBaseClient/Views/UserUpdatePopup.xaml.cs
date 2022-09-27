using DataBaseClient.Models;

namespace DataBaseClient.Views;

public partial class UserUpdatePopup : CommunityToolkit.Maui.Views.Popup
{
	public UserUpdatePopup()
	{
		InitializeComponent();
        ResultWhenUserTapsOutsideOfPopup = null;
    }

    void OnYesButtonClicked(object sender, EventArgs e)
    {
        Guid guid = new Guid();

        Guid.TryParse(guidEntry.Text, out guid);

        Close(new User
        {
            Guid = guid,
            Login = loginEntry.Text,
            Email = emailEntry.Text,
            PhoneNumber = phoneEntry.Text
        });
    }

    void OnNoButtonClicked(object sender, EventArgs e) => Close(null);
}