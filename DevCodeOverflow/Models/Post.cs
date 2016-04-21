using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCodeOverflow.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public DateTime DatePosted { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}