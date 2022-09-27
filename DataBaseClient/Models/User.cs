using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace DataBaseClient.Models;

public partial class User : ObservableObject
{
    [ObservableProperty]
    public Guid guid;
    [ObservableProperty]
    public string login;
    [ObservableProperty]
    public string email;
    [ObservableProperty]
    public string phoneNumber;

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
