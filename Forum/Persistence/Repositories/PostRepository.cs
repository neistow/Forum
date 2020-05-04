using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Forum.Core.Domain;
using Forum.Core.Repositories;

namespace Forum.Persistence.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetPostsWithReplies()
        {
            return ApplicationDbContext.Posts.Include(p => p.Replies).ToList();
        }

        public Post GetPostWithReplies(int id)
        {
            return ApplicationDbContext.Posts.Include(p => p.Replies).SingleOrDefault(p => p.Id == id);
        }

        private ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
    }
}