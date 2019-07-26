using IMASD.DATA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IMASD.DATA.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private MainContext _context;

        private readonly IDbSet<T> _dbSet;

        protected RepositoryBase(MainContext context)
        {
            _context = context;
            _dbSet = DataContext.Set<T>();
        }

        protected RepositoryBase()
        {
            _dbSet = DataContext.Set<T>();
        }

        protected MainContext DataContext
        {
            get { return _context ?? (_context = new MainContext()); }
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
            this.DataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            this.DataContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetByID(object id)
        {
            return _dbSet.Find(id);
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }
        public int Count(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        } 
        
    }
}
