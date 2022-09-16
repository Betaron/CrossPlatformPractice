using CommunityToolkit.Maui.Views;
using DataBaseClient.Gateways.Users;
using DataBaseClient.Models;
using DataBaseClient.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace DataBaseClient.ViewModels;

public class UsersViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; } = new();
    private DataContext _context;
    private IUserRepository _userRepository;

    public ICommand CreateUserCommand { get; private set; }
    public ICommand CopyToClipboardCommand { get; private set; }

    public UsersViewModel(
        DataContext context,
        IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;

        CreateUserCommand = new Command(CreateUser);
        CopyToClipboardCommand = new Command<string>(CopyToClipboard);
    }

    async void CopyToClipboard(string guid)
    {
        await Clipboard.Default.SetTextAsync(guid);
    }

    async void CreateUser()
    {
        var popup = new UserCreatePopup();
        await PopupExtensions.ShowPopupAsync<UserCreatePopup>(
            Application.Current.MainPage, popup);

        User user = await popup?.Result as User;

        if (user is null)
            return;

        user.Guid = Guid.NewGuid();

        WrapInExceptionHandler(() =>
        {
            _userRepository.Create(user);
            Users.Add(user);
        });
    }

    void GetUsers()
    {
        WrapInExceptionHandler(() =>
        {
            //var users = _userRepository.GetAllUsers();

            var users = _userRepository.GetAllUsers().Select(x =>
            new User
            {
                Guid = x.Key,
                Login = x.Value.Login,
                Email = x.Value.Email,
                PhoneNumber = x.Value.PhoneNumber
            });

            if (Users.Count != 0)
                Users.Clear();

            foreach (var user in users)
            {
                Users.Add(user);
            }
        });
    }


}
