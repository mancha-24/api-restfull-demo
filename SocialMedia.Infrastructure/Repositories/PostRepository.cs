using System.Collections;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post 
            {
               PostId = x,
               Description = $"Description: {x}",
               Date = DateTime.Now,
               Image = $"https://misapis.com/{x}",
               UserId = x * 2 
            });
            await Task.Delay(10);

            return posts;
        }

    }
}