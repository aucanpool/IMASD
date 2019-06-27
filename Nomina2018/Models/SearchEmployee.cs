using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina2018.Models
{
    public class SearchEmployee
    {
        public string FullName { get; set; }
        public string KeyEmplooye { get; set; }
        public int? DepartamentId { get; set; }
        public int? SalaryTabulatorId { get; set; }
    }
}