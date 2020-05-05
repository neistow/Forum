using System.Collections.Generic;
using Forum.Core.Domain;

namespace Forum.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}