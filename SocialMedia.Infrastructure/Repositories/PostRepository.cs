using System.Collections;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();

            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(c => c.PostId == id);
            return post;
        }

    }
}