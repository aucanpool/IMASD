using IMASD.DATA.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IEmployeeBussinesLogic: IEmployeeService
    {
        IEnumerable<Employee> getByFullNameAndJobNumberAndDepartamentAndSalaryTabulador(string jobNumber,string fullName, int? departamentId, int? salaryTabulatorId);
    }
}
