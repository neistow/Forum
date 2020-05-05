using System;
using System.Collections.Generic;
using Forum.Core.Domain;

namespace Forum.Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPostsWithAuthors();
        Post GetPostWithAuthor(int id);
    }
}