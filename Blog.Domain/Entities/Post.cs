using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset ModifiedAt { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        //public string CommentId { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string UserId { get; set; }
        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
