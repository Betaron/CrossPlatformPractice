using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users.Repositories;

internal class UserRepository : IUserRepository
{
    void IUserRepository.Create(User user)
    {
        throw new NotImplementedException();
    }

    void IUserRepository.Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<User> IUserRepository.GetAllUsers()
    {
        throw new NotImplementedException();
    }

    User IUserRepository.GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    void IUserRepository.Update(Guid id, User user)
    {
        throw new NotImplementedException();
    }
}
