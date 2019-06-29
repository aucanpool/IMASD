using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEmployeeService: IService<Employee>
    {
        IEnumerable<Employee> GetByFilters(string jobNumber, string fulltName, int? departamentId, int? salaryTabulatorId);
        IEnumerable<Employee> GetByFilters(string inWhatEverColumn);
    }
}
