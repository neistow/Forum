using System.Collections.Generic;
using Forum.Core.Domain;

namespace Forum.Core.Repositories
{
    public interface IReplyRepository : IRepository<Reply>
    {
        IEnumerable<Reply> GetAllRepliesToPost(int postId);
        Reply GetReplyWithAuthor(int replyId);
    }
}