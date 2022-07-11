using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
         Task<IEnumerable<Post>> GetPostByUser(int userId);
    }
}