using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(int id);

        Task InsertUser(User post);

        Task<bool> UpdateUser(User post);
        Task<bool> DeleteUser(int id);
    }
}