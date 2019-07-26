using IMASD.Base.DataTablesDTO;
using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPaymentService: IService<Payment>
    {
        DataTableOutput<Payment> GetByFilters( string employee, int? departamentId,  DataTableInput input);
    }
}
