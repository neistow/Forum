using System;
using Forum.Models;

namespace Forum.Core.Domain
{
    public class Reply
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        
        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }
    }
}