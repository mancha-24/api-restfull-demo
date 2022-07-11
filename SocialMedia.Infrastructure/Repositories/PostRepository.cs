using System.Collections;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetPostByUser(int userId)
        {
            return await _entities.Where(c => c.UserId == userId).ToListAsync();
        }
    }
}