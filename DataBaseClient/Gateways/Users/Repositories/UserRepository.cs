using DataBaseClient.Exceptions;
using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users.Repositories;

public class UserRepository : IUserRepository
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

        _context.Users.Add(user.Guid, new UserDbModel(user));
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

    Dictionary<Guid, UserDbModel> IUserRepository.GetAllUsers()
    {
        if (_context.Users.Count == 0)
        {
            throw new ValidationException(
                "Users aren't in the database.");
        }

        return _context.Users;
    }

    KeyValuePair<Guid, UserDbModel> IUserRepository.GetUserById(Guid id)
    {
        if (!_context.Users.ContainsKey(id))
        {
            throw new ValidationException(
                $"User with Id \"{id}\" doesn't exist.");
        }

        return new KeyValuePair<Guid, UserDbModel>(id, _context.Users[id]);
    }

    KeyValuePair<Guid, UserDbModel> IUserRepository.GetUserByLogin(string login)
    {
        var entity = _context.Users.FirstOrDefault(
            it => it.Value.Login == login);

        if (entity.Value is null)
        {
            throw new ValidationException(
                $"User with login \"{login}\" doesn't exist.");
        }

        return entity;
    }

    void IUserRepository.Update(User user)
    {
        if (!_context.Users.ContainsKey(user.Guid))
        {
            throw new ValidationException(
                $"User with Id \"{user.Guid}\" doesn't exist.");
        }

        var sameLoginEntity = _context.Users.FirstOrDefault(
            it => it.Value.Login == user.Login && it.Key != user.Guid);

        if (sameLoginEntity.Value is not null)
        {
            throw new ValidationException(
                $"User with Login \"{user.Login}\" already exists.");
        }

        _context.Users.Add(user.Guid, new UserDbModel(user));
    }
}
