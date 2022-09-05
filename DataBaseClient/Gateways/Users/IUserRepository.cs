using DataBaseClient.Models;

namespace DataBaseClient.Gateways.Users;

internal interface IUserRepository
{
    /// <summary>
    /// The method is designed to return a user object by its unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>The user with the passed UID.</returns>
    internal Task<User> GetUserByIdAsync(Guid id);

    /// <summary>
    /// The method finds and returns all users from the repository.
    /// </summary>
    /// <returns>Collection of all users.</returns>
    internal Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>
    /// Creates a user object and adds it to storage.
    /// </summary>
    /// <param name="user">User preimage to add.</param>
    internal Task CreateAsync(User user);

    /// <summary>
    /// Updates the user data in the database by the passed unique identifier
    /// based on the passed user object.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="user">User preimage to update.</param>
    internal Task UpdateAsync(Guid id, User user);

    /// <summary>
    /// Deletes a user by their unique ID.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    internal Task DeleteUserAsync(Guid id);
}
