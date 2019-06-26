using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina2018.Models
{
    public class JobDTO
    {
        public int Id { get;  set; }
        
        public string Key { get; set; }

        public string Name { get; set; }
        public virtual ICollection<SalaryTabulatorDTO> SalaryTabulators { get; set; }
    }
}