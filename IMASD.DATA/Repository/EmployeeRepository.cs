using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using IMASD.Base.ENUMS;
using IMASD.Base.Utilities;
using System.Linq.Expressions;

namespace IMASD.DATA.Repository
{
    public class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MainContext context)
            :base(context)
        {

        }

        public IEnumerable<Employee> GetByFilters(string inWhatEverColumn)
        {
            var query = DataContext.Employees.AsQueryable();
            query = query.Where(
                x=>x.JobNumber.Contains(inWhatEverColumn) || x.FirstName.Contains(inWhatEverColumn)
                || x.LastName.Contains(inWhatEverColumn)||x.Address.Contains(inWhatEverColumn) 
                ||x.Telefone.Contains(inWhatEverColumn));

            return query.ToList();
        }

        public IEnumerable<Employee> GetByFilters(int? Id, string jobNumber, string firstName, string lastName, string address, string telefone, Gender? gender, DateTime? startHireDate, DateTime? endHireDate, bool? active, int? departamentId, int? salaryTabulatorId)
        {
            var query = DataContext.Employees.AsQueryable();
            if (Id != null && Id > 0)
                query = query.Where(x=>x.Id==Id);
            if (!string.IsNullOrEmpty(jobNumber))
                query = query.Where(x => x.JobNumber.Contains(jobNumber));
            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.LastName.Contains(lastName));
            if (!string.IsNullOrEmpty(address))
                query = query.Where(x => x.Address.Contains(address));
            if (!string.IsNullOrEmpty(telefone))
                query = query.Where(x => x.Telefone.Contains(telefone));
            if (gender!=null)
                query = query.Where(x => x.Gender==gender);

            if (startHireDate != null && endHireDate!=null)
                query = query.Where(x =>  x.HireDate >=startHireDate && x.HireDate<=endHireDate);
            else if (startHireDate != null && endHireDate == null)
                query = query.Where(x => x.HireDate >= startHireDate);
            else if (startHireDate == null && endHireDate != null)
                query = query.Where(x => x.HireDate <= endHireDate);
            if (active != null)
                query = query.Where(x => x.Active == active);
            if (departamentId != null && departamentId>0)
                query = query.Where(x => x.DepartamentId == departamentId);
            if (salaryTabulatorId != null && salaryTabulatorId > 0)
                query = query.Where(x => x.SalaryTabulatorId == salaryTabulatorId);
            
            return query.ToList();

        }
        public IQueryable<Employee> GetQueryByFilters(int? Id, string jobNumber, string firstName, string lastName, string address, string telefone, Gender? gender, DateTime? startHireDate, DateTime? endHireDate, bool? active, int? departamentId, int? salaryTabulatorId)
        {
            var query = DataContext.Employees.AsQueryable();
            if (Id != null && Id > 0)
                query = query.Where(x => x.Id == Id);
            if (!string.IsNullOrEmpty(jobNumber))
                query = query.Where(x => x.JobNumber.Contains(jobNumber));
            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.LastName.Contains(lastName));
            if (!string.IsNullOrEmpty(address))
                query = query.Where(x => x.Address.Contains(address));
            if (!string.IsNullOrEmpty(telefone))
                query = query.Where(x => x.Telefone.Contains(telefone));
            if (gender != null)
                query = query.Where(x => x.Gender == gender);

            if (startHireDate != null && endHireDate != null)
                query = query.Where(x => x.HireDate >= startHireDate && x.HireDate <= endHireDate);
            else if (startHireDate != null && endHireDate == null)
                query = query.Where(x => x.HireDate >= startHireDate);
            else if (startHireDate == null && endHireDate != null)
                query = query.Where(x => x.HireDate <= endHireDate);
            if (active != null)
                query = query.Where(x => x.Active == active);
            if (departamentId != null && departamentId > 0)
                query = query.Where(x => x.DepartamentId == departamentId);
            if (salaryTabulatorId != null && salaryTabulatorId > 0)
                query = query.Where(x => x.SalaryTabulatorId == salaryTabulatorId);
            
            return query;

        }
        public Expression<Func<Employee, bool>> GetPredicateByFilters(int? Id, string jobNumber, string firstName, string lastName, string address, string telefone, Gender? gender, DateTime? startHireDate, DateTime? endHireDate, bool? active, int? departamentId, int? salaryTabulatorId)
        {
            var predicate = PredicateBuilder.True<Employee>();
            if (Id != null && Id > 0)
                predicate = predicate.And(x => x.Id == Id);
            if (!string.IsNullOrEmpty(jobNumber))
                predicate = predicate.And(x => x.JobNumber.Contains(jobNumber));
            if (!string.IsNullOrEmpty(firstName))
                predicate = predicate.And(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrEmpty(lastName))
                predicate = predicate.And(x => x.LastName.Contains(lastName));
            if (!string.IsNullOrEmpty(address))
                predicate = predicate.And(x => x.Address.Contains(address));
            if (!string.IsNullOrEmpty(telefone))
                predicate = predicate.And(x => x.Telefone.Contains(telefone));
            if (gender != null)
                predicate = predicate.And(x => x.Gender == gender);

            if (startHireDate != null && endHireDate != null)
                predicate = predicate.And(x => x.HireDate >= startHireDate && x.HireDate <= endHireDate);
            else if (startHireDate != null && endHireDate == null)
                predicate = predicate.And(x => x.HireDate >= startHireDate);
            else if (startHireDate == null && endHireDate != null)
                predicate = predicate.And(x => x.HireDate <= endHireDate);
            if (active != null)
                predicate = predicate.And(x => x.Active == active);
            if (departamentId != null && departamentId > 0)
                predicate = predicate.And(x => x.DepartamentId == departamentId);
            if (salaryTabulatorId != null && salaryTabulatorId > 0)
                predicate = predicate.And(x => x.SalaryTabulatorId == salaryTabulatorId);

            return predicate;

        }
        private Expression<Func<Employee, bool>> BuildDynamicWhereClause(string searchValue)
        {
            var predicate = PredicateBuilder.True<Employee>();
            if (String.IsNullOrWhiteSpace(searchValue) == false)
            {
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
                predicate = predicate.Or(s => searchTerms.Any(srch => s.JobNumber.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.FirstName.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.LastName.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Address.ToLower().Contains(srch)));
                predicate = predicate.Or(s => searchTerms.Any(srch => s.Telefone.ToLower().Contains(srch)));
            }
            return predicate;
        }
        public IEnumerable<Employee> SortOrderAndPaging(Expression<Func<Employee, bool>> where, int take, int skip, string sortBy, bool sortDir)
        {
            var whereClause = DataContext.Employees.Include("Departament").Include("SalaryTabulator").AsQueryable().Where(where);

            if (String.IsNullOrEmpty(sortBy))
            {
                sortBy = "Id";
                sortDir = true;
            }
            string sort = sortDir ? "ascending" : "descending";
            IEnumerable<Employee> result =whereClause
                .OrderBy(sortBy + " " + sort)
                           .Skip(skip)
                           .Take(take)
                           .ToList<Employee>();
            return result;
        }
    }
}
