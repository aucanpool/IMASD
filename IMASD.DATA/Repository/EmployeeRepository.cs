using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMASD.Base.ENUMS;

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
    }
}
