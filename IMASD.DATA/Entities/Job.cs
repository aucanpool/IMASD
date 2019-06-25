using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Entities
{
    public class Job
    {
        public int Id{ get; private set; }

        public string Key{ get; set; }

        public string Name{ get; set; }
        public virtual ICollection<SalaryTabulator> SalaryTabulators { get; set; }

    }
}
