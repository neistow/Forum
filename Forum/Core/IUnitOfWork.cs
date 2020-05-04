using System;
using Forum.Core.Repositories;

namespace Forum.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IReplyRepository Replies { get; }
        int Complete();
    }
}