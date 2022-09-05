using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users;

internal interface IUserRepository
{
    /// <summary>
    /// The method is designed to return a user object by its unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>The user with the passed UID.</returns>
    internal User GetUserById(Guid id);

    /// <summary>
    /// The method finds and returns all users from the repository.
    /// </summary>
    /// <returns>Collection of all users.</returns>
    internal IEnumerable<User> GetAllUsers();

    /// <summary>
    /// Creates a user object and adds it to storage.
    /// </summary>
    /// <param name="user">User preimage to add.</param>
    internal void Create(User user);

    /// <summary>
    /// Updates the user data in the database by the passed unique identifier
    /// based on the passed user object.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="user">User preimage to update.</param>
    internal void Update(Guid id, User user);

    /// <summary>
    /// Deletes a user by their unique ID.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    internal void Delete(Guid id);
}
