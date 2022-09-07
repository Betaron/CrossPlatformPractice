using DataBaseClient.Models;

namespace DataBaseClient;

internal class DataContext
{
    public Dictionary<Guid, User> Users { get; private set; }

    DataContext()
    {
        Users = new Dictionary<Guid, User>();
    }

    public bool UpdateUser(User newUser)
    {
        if (!Users.ContainsKey(newUser.Id))
        {
            return false;
        }

        Users[newUser.Id] = new User(newUser);

        return true;
    }
}
