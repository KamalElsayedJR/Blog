using Blog.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure
{
    public static class SpecificationEvalutor<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery,ISpecification<T> Spec)
        {
            var Query = InputQuery;
            if (Spec.Condition is not null)
            {
                Query = Query.Where(Spec.Condition);
            }
            Query = Spec.Includes.Aggregate(Query ,(CurrentQuery,IncludeExpresstion)=> CurrentQuery.Include(IncludeExpresstion));
            return Query;
        }
    }
}
