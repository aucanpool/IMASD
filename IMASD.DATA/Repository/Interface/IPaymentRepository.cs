using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Repository.Interface
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        IEnumerable<Payment> SortOrderAndPaging(Expression<Func<Payment, bool>> where, int take, int skip, string sortBy, bool sortDir);
        Expression<Func<Payment, bool>> GetPredicateByFilters( string employee, int? departamentId);
    }
}
