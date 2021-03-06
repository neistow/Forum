﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Models;

namespace Forum.Core.Domain
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }

        public IEnumerable<Reply> Replies { get; set; }
    }
}