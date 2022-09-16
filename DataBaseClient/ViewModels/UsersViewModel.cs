using CommunityToolkit.Maui.Views;
using DataBaseClient.Gateways.Users;
using DataBaseClient.Models;
using DataBaseClient.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataBaseClient.ViewModels;

public class UsersViewModel : BaseViewModel
{
    public ObservableCollection<KeyValuePair<Guid, User>> Users { get; } = new();
    private DataContext _context;
    private IUserRepository _userRepository;

    public ICommand CreateUserCommand { get; private set; }

    public UsersViewModel(
        DataContext context,
        IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;

        CreateUserCommand = new Command(CreateUser);
    }

    async void CreateUser()
    {
        var popup = new UserCreatePopup();
        await PopupExtensions.ShowPopupAsync<UserCreatePopup>(
            Application.Current.MainPage, popup);

        User user = await popup?.Result as User;

        if (user is null)
            return;

        WrapInExceptionHandler(() =>
        {
            _userRepository.Create(user);

            Users.Add(_userRepository.GetUserByLogin(user.Login));
        });
    }

    void GetUsers()
    {
        WrapInExceptionHandler(() =>
        {
            var users = _userRepository.GetAllUsers();

            if (Users.Count != 0)
                Users.Clear();

            foreach (var user in users)
            {
                Users.Add(user);
            }
        });
    }


}
