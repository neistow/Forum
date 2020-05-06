using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Forum.Core.Domain;
using Forum.Core.Repositories;

namespace Forum.Persistence.Repositories
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        private ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public ReplyRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Reply> GetAllRepliesToPost(int postId)
        {
            return ApplicationDbContext.Replies.Where(r => r.Post.Id == postId).Include(r => r.Author);
        }

        public Reply GetReplyWithAuthor(int replyId)
        {
            return ApplicationDbContext.Replies.Include(r => r.Author).SingleOrDefault(r => r.Id == replyId);
        }
    }
}