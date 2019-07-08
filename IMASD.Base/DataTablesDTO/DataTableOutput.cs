using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMASD.Base.DataTablesDTO
{
    public class DataTableOutput<T> where T : class
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }

    }
}