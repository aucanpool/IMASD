using IMASD.Base.Utilities;
using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Repository
{
    public class PaymentRepository: RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(MainContext context)
            :base(context)
        {
        }
        public Expression<Func<Payment, bool>> GetPredicateByFilters(string employee, int? departamentId)
        {
            var predicate = PredicateBuilder.True<Payment>();
            if (!String.IsNullOrWhiteSpace(employee))
            {
                var searchTerms = employee.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.And(s => searchTerms.Any(srch => s.Employee.FirstName.Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Employee.LastName.Contains(srch)));
            }
            if (departamentId != null && departamentId > 0)
                predicate = predicate.And(x => x.Employee.DepartamentId == departamentId);
            
            return predicate;

        }
        public IEnumerable<Payment> SortOrderAndPaging(Expression<Func<Payment, bool>> where, int take, int skip, string sortBy, bool sortDir)
        {
            var whereClause = DataContext.Payments.Include("Employee").AsQueryable().Where(where);

            if (String.IsNullOrEmpty(sortBy))
            {
                sortBy = "Id";
                sortDir = true;
            }
            string sort = sortDir ? "ascending" : "descending";
            IEnumerable<Payment> result = whereClause
                .OrderBy(sortBy + " " + sort)
                           .Skip(skip)
                           .Take(take)
                           .ToList<Payment>();
            return result;
        }

    }
}
