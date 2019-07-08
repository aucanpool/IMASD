
using IMASD.DATA.Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using IMASD.DATA.Entities;
using System.Linq.Expressions;
using IMASD.Base.DataTablesDTO;

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

        

        public IEnumerable<Employee> GetByFilters(string inWhatEverColumn , DataTableInput input)
        {
            return repository.GetByFilters(inWhatEverColumn);
        }

        public IEnumerable<Employee> GetByFilters(string jobNumber, string name, int? departamentId, int? salaryTabulatorId)
        {
            return repository.GetByFilters(null,jobNumber,name,"","","",null,null,null,null,departamentId,salaryTabulatorId);
        }


        public DataTableOutput<Employee> GetByFilters(string jobNumber, string name, int? departamentId, int? salaryTabulatorId, DataTableInput input)
        {
            var output = new DataTableOutput<Employee>();
            var query = repository.GetPredicateByFilters(null, jobNumber, name, "", "", "", null, null, null, null, departamentId, salaryTabulatorId);
            string sortBy = "";
            bool sortDir = true;

            if (input.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = input.columns[input.order[0].column].data;
                sortDir = input.order[0].dir.ToLower() == "asc";
            }
            IEnumerable<Employee> result= repository.SortOrderAndPaging(query, input.length, input.start, sortBy, sortDir);
            
            if (result == null)
            {
                output.data= new List<Employee>();
            }
            output.data = result.ToList();
            output.recordsFiltered = repository.Count(query);
            output.recordsTotal = repository.Count(x=>x.Active==true);
            return output;
        }
    }
}
