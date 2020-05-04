using Forum.Core;
using Forum.Core.Repositories;
using Forum.Persistence.Repositories;

namespace Forum.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPostRepository Posts { get; }
        public IReplyRepository Replies { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Posts = new PostRepository(_context);
            Replies = new ReplyRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}