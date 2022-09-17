using DataBaseClient.Models;

namespace DataBaseClient.Views;

public partial class UserDeletePopup : CommunityToolkit.Maui.Views.Popup
{
	public UserDeletePopup()
	{
		InitializeComponent();
        ResultWhenUserTapsOutsideOfPopup = null;
    }

    void OnYesButtonClicked(object sender, EventArgs e)
    {
        Guid guid = new Guid();

        Guid.TryParse(guidEntry.Text, out guid);

        Close(guid);
    }

    void OnNoButtonClicked(object sender, EventArgs e) => Close(null);
}