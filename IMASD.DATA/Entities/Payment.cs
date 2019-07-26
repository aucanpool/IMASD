using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public FrequencyofPayments FrequencyofPayments { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
