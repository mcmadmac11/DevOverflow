using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DevCodeOverflow.Models;

namespace DevCodeOverflow.Hubs
{
    public class BoardHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void WritePost(string username, string message)
        {
            Post post = new Post {Message = message, Username = username, DatePosted = DateTime.Now };
            db.Posts.Add(post);
            db.SaveChanges();

            Clients.All.receivedNewPost(post.Id, post.Username, post.Message, post.DatePosted);
        }

        public void AddComment(int postId, string comment, string username)
        {
            Post post = db.Posts.FirstOrDefault(p => p.Id == postId);

            if (post != null)
            {
                var newComment = new Comment { ParentPost = post, Message = comment, Username = username, DatePosted = DateTime.Now};
                db.Comments.Add(newComment);
                db.SaveChanges();

                Clients.All.receivedNewComment(newComment.ParentPost.Id, newComment.Id, newComment.Message, newComment.DatePosted);
            }
        }
    }
}