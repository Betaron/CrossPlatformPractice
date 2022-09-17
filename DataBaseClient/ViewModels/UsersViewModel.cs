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
    public ICommand UpdateUserCommand { get; private set; }
    public ICommand DeleteUserCommand { get; private set; }

    public UsersViewModel(
        DataContext context,
        IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;

        CreateUserCommand = new Command(CreateUser);
        UpdateUserCommand = new Command(UpdateUser);
        DeleteUserCommand = new Command(DeleteUser);
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

    async void UpdateUser()
    {
        var popup = new UserUpdatePopup();
        await PopupExtensions.ShowPopupAsync<UserUpdatePopup>(
            Application.Current.MainPage, popup);

        User user = await popup?.Result as User;

        if (user is null || user?.Guid == Guid.Empty)
            return;

        WrapInExceptionHandler(() =>
        {
            _userRepository.Update(user);
            var entity = Users.FirstOrDefault(x => x.Guid == user.Guid);

            entity.Login = user.Login;
            entity.Email = user.Email;
            entity.PhoneNumber = user.PhoneNumber;
        });
    }

    async void DeleteUser()
    {
        var popup = new UserDeletePopup();
        await PopupExtensions.ShowPopupAsync<UserDeletePopup>(
            Application.Current.MainPage, popup);

        Guid? guid = await popup?.Result as Guid?;

        if (guid is null || guid == Guid.Empty)
            return;

        WrapInExceptionHandler(() =>
        {
            _userRepository.Delete((Guid)guid);
            var entity = Users.FirstOrDefault(x => x.Guid == (Guid)guid);

            Users.Remove(entity);
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
