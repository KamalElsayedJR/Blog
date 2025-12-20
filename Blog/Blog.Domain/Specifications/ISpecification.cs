using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T,bool>> Condition { get; set; }

        public List<Expression<Func<T,object>>> Includes { get; set; }

    }
}
