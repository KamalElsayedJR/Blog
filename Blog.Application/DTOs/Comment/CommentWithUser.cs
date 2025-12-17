using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.Comment
{
    public class CommentWithUser
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}
