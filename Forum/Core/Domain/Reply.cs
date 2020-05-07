using System;
using System.ComponentModel.DataAnnotations;
using Forum.Models;

namespace Forum.Core.Domain
{
    public class Reply
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public Post Post { get; set; }
    }
}