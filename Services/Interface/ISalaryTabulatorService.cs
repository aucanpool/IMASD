using IMASD.Base.DataTablesDTO;
using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISalaryTabulatorService: IService<SalaryTabulator>
    {
        DataTableOutput<SalaryTabulator> GetSalarysTabulators(DataTableInput input);
    }
}
