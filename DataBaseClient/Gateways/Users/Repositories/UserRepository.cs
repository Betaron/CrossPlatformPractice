using DataBaseClient.Exceptions;
using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    void IUserRepository.Create(User user)
    {
        var entity = _context.Users.FirstOrDefault(
            it => it.Value.Login == user.Login);

        if (entity.Value is not null)
        {
            throw new ValidationException(
                $"User with Login \"{user.Login}\" already exists.");
        }

        _context.Users.Add(user.Id, user);
    }

    void IUserRepository.Delete(Guid id)
    {
        if (!_context.Users.ContainsKey(id))
        {
            throw new ValidationException(
                $"User with Id \"{id}\" doesn't exist.");
        }

        _context.Users.Remove(id);
    }

    IEnumerable<User> IUserRepository.GetAllUsers()
    {
        if (_context.Users.Count == 0)
        {
            throw new ValidationException(
                "Users aren't in the database.");
        }

        return _context.Users.Values.ToList();
    }

    User IUserRepository.GetUserById(Guid id)
    {
        if (!_context.Users.ContainsKey(id))
        {
            throw new ValidationException(
                $"User with Id \"{id}\" doesn't exist.");
        }

        return _context.Users[id];
    }

    void IUserRepository.Update(Guid id, User user)
    {
        if (!_context.Users.ContainsKey(id))
        {
            throw new ValidationException(
                $"User with Id \"{id}\" doesn't exist.");
        }

        var sameLoginEntity = _context.Users.FirstOrDefault(
            it => it.Value.Login == user.Login && it.Value.Id != user.Id);

        if (sameLoginEntity.Value is not null)
        {
            throw new ValidationException(
                $"User with Login \"{user.Login}\" already exists.");
        }

        _context.UpdateUser(user);
    }
}
