using System.Data.Entity;
using Forum.Core.Domain;
using Forum.Core.Repositories;

namespace Forum.Persistence.Repositories
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        public ReplyRepository(DbContext context) : base(context)
        {
        }
        
    }
}