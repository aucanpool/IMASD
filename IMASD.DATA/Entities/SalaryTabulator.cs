using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Entities
{
    public class SalaryTabulator
    {
        public int Id{ get; private set; }
        public string Key{ get; set; }
        public int JobId { get; set; }
        public virtual Job Job{ get; set; }
        public TabulatorLevel TabulatorLevel{ get; set; }
        public float Hourlywages{ get; set; }
        public float AnnualHolidayBonus{ get; set; }
        public int AnnualBonusDays{ get; set; }
        public int AnnualVacationDays{ get; set; }
        public bool Active { get; set; }
        //Navigation properties
        public virtual ICollection<Employee> Employees { get; set;}


    }
}
