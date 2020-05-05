using System.Collections.Generic;
using System.Data.Entity;
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
            return null;
        }
    }
}