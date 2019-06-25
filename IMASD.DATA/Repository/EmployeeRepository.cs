using IMASD.DATA.Entities;
using IMASD.DATA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Repository
{
    public class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MainContext context)
            :base(context)
        {

        }
    }
}
