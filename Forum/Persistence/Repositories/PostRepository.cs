using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Forum.Core.Domain;
using Forum.Core.Repositories;

namespace Forum.Persistence.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public PostRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetPostsWithAuthors()
        {
            return ApplicationDbContext.Posts.Include(p => p.Replies).ToList();
        }

        public Post GetPostWithAuthor(int id)
        {
            return ApplicationDbContext.Posts.Include(p => p.Author).SingleOrDefault(p => p.Id == id);
        }
    }
}