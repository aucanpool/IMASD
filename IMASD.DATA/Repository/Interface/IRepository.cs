using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Repository.Interface
{
    public interface IRepository<T> where T :class 
    {
        T GetByID(object id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        void Insert(T entity);
        void Update(T entity);
        
    }
}
