using IMASD.Base.DataTablesDTO;
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
        IEnumerable<Employee> GetByFilters(string jobNumber, string name, int? departamentId, int? salaryTabulatorId);
        DataTableOutput<Employee> GetByFilters(string jobNumber, string name, int? departamentId, int? salaryTabulatorId, DataTableInput input);
        IEnumerable<Employee> GetByFilters(string inWhatEverColumn, DataTableInput input);
        
        
    }
}
