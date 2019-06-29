using IMASD.Base.ENUMS;
using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Repository.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetByFilters(int? Id, string jobNumber, string firstName, string lastName, string address,string telefone,Gender? gender, DateTime? startHireDate, DateTime? endHireDate, bool? active, int? departamentId, int? salaryTabulatorId );
        IEnumerable<Employee> GetByFilters(string inWhatEverColumn);
    }
}
