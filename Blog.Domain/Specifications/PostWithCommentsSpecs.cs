using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Specifications
{
    public class PostWithCommentsSpecs: BaseSpecification<Post>
    {
         
        public PostWithCommentsSpecs(): base()
        {
            Includes.Add(P => P.Comments);
            Includes.Add(P => P.Category);
        }
        public PostWithCommentsSpecs(string id):base(p=>p.Id == id)
        {
            Includes.Add(P => P.Comments);
            Includes.Add(P => P.Category);
        }
    }
}
