using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users;

public interface IUserRepository
{
    /// <summary>
    /// The method is designed to return a user object by its unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>The user with the passed UID.</returns>
    public KeyValuePair<Guid, UserDbModel> GetUserById(Guid id);

    /// <summary>
    /// The method is designed to return a user object by its login.
    /// </summary>
    /// <param name="login">User login</param>
    /// <returns>The user with the passed login.</returns>
    public KeyValuePair<Guid, UserDbModel> GetUserByLogin(string login);

    /// <summary>
    /// The method finds and returns all users from the repository.
    /// </summary>
    /// <returns>Collection of all users.</returns>
    public Dictionary<Guid, UserDbModel> GetAllUsers();

    /// <summary>
    /// Creates a user object and adds it to storage.
    /// </summary>
    /// <param name="user">User preimage to add.</param>
    public void Create(User user);

    /// <summary>
    /// Updates the user data in the database by the passed unique identifier
    /// based on the passed user object.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="user">User preimage to update.</param>
    public void Update(User user);

    /// <summary>
    /// Deletes a user by their unique ID.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    public void Delete(Guid id);
}
