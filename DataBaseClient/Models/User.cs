using System.Windows.Input;

namespace DataBaseClient.Models;

public class User
{
    public Guid Guid { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public ICommand CopyToClipboardCommand { get; private set; }

    public User()
    {
        CopyToClipboardCommand = new Command<string>(CopyToClipboard);
    }
    async void CopyToClipboard(string guid)
    {
        await Clipboard.Default.SetTextAsync(guid);
    }
}
