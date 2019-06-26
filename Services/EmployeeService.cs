using IMASD.DATA.Repository;
using IMASD.DATA.Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using IMASD.DATA.Entities;
using System.Linq.Expressions;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        
        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
         public Employee GetByID(object id)
        {
            return this.repository.GetByID(id);
        }

        public Employee Get(Expression<Func<Employee, bool>> where)
        {
            return this.repository.Get(where);
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Employee> GetMany(Expression<Func<Employee, bool>> where)
        {
            return this.repository.GetMany(where);
        }

        public void Insert(Employee entity)
        {
            this.repository.Insert(entity);
        }

        public void Update(Employee entity)
        {
            this.repository.Update(entity);
        }

        public void Delete(Employee entity)
        {
            entity.Active = false;
            this.repository.Update(entity);
        }

        public void Delete(object id)
        {
            var entity = GetByID(id);
            entity.Active = false;
            this.repository.Update(entity);
        }
    }
}
