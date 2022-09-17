using DataBaseClient.Models;

namespace DataBaseClient.Views;

public partial class SavePopup : CommunityToolkit.Maui.Views.Popup
{
	public SavePopup()
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