using DataBaseClient.Gateways;
using DataBaseClient.Models;

namespace DataBaseClient;

public class DataContext
{
    private Dictionary<Guid, UserDbModel> _users = new();
    public Dictionary<Guid, UserDbModel> Users
    {
        get => _users;
        set
        {
            _users = value;
        }
    }
}
