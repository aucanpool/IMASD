using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Entities
{
    public class Departament
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
