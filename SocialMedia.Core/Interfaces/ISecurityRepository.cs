using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}