using DataBaseClient.Models;

namespace DataBaseClient;

public class DataContext
{
    private Dictionary<Guid, User> _users = new();
    public Dictionary<Guid, User> Users
    {
        get => _users;
        set
        {
            _users = value;
        }
    }
}
