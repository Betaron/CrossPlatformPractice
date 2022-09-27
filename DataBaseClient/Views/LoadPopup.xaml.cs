using DataBaseClient.Models;

namespace DataBaseClient.Views;

public partial class LoadPopup : CommunityToolkit.Maui.Views.Popup
{
	public LoadPopup()
	{
		InitializeComponent();
        ResultWhenUserTapsOutsideOfPopup = null;
    }

    void OnYesButtonClicked(object sender, EventArgs e)
    {
        Close(saveEntry.Text);
    }

    void OnNoButtonClicked(object sender, EventArgs e) => Close(null);
}