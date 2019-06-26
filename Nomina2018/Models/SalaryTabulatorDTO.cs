using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina2018.Models
{
    public class SalaryTabulatorDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int JobId { get; set; }
        public virtual JobDTO Job { get; set; }
        public TabulatorLevel TabulatorLevel { get; set; }
        public float Hourlywages { get; set; }
        public float AnnualHolidayBonus { get; set; }
        public int AnnualBonusDays { get; set; }
        public int AnnualVacationDays { get; set; }
        //Navigation properties
        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}