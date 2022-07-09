using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post 
            {
               PostId = x,
               Description = $"Description: {x}",
               Date = DateTime.Now,
               Image = $"https://misapis.com/{x}",
               UserId = x * 2 
            });

            return posts;
        }
    }
}