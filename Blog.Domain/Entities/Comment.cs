using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string PostId { get; set; }
        public Post Post{ get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
