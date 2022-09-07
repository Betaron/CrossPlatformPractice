namespace DataBaseClient.Models;

internal class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public User(User InstanceToCopy)
    {
        Id = InstanceToCopy.Id;
        Login = InstanceToCopy.Login;
        Email = InstanceToCopy.Email;
        PhoneNumber = InstanceToCopy.PhoneNumber;
    }
}
