using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users.Repositories
{
    internal class UserRepository : IUserRepository
    {
        Task IUserRepository.CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.Deleteuser(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<User>> IUserRepository.GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        Task<User> IUserRepository.GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IUserRepository.UpdateAsync(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
