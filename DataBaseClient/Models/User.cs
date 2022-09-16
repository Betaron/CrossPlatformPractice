namespace DataBaseClient.Models;

public class User
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public User() { }

    public User(User InstanceToCopy)
    {
        Login = InstanceToCopy.Login;
        Email = InstanceToCopy.Email;
        PhoneNumber = InstanceToCopy.PhoneNumber;
    }
}
