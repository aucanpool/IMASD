using IMASD.Base.DataTablesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina2018.Models.JSON
{
    public class DataTableVM<T> where T :class
    {
        public T Filters { get; set; }
        public DataTableInput dataTablesInput { get; set; }
    }
}