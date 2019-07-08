using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMASD.DATA.DTO
{
    public class SalaryTabulatorDTO
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }

        [Display(Name = "Clave")]
        [StringLength(100)]
        public string Key { get; set; }

        [Display(Name = "Puesto")]
        public int JobId { get; set; }
        
        public virtual JobDTO Job { get; set; }

        [Display(Name = "Nivel")]
        public TabulatorLevel TabulatorLevel { get; set; }

        [Display(Name = "Salario por día")]
        [DataType(DataType.Currency, ErrorMessage = "Formato invalido")]
        public float Hourlywages { get; set; }

        [Display(Name = "Prima vacacional anual")]
        [DataType(DataType.Currency, ErrorMessage = "Formato invalido")]
        public float AnnualHolidayBonus { get; set; }
        [Display(Name = "Dias de aguinaldo anual")]
        public int AnnualBonusDays { get; set; }

        [Display(Name = "Días de vacaciones anuales")]
        public int AnnualVacationDays { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }
        //Navigation properties
        public virtual ICollection<EmployeeDTO> Employees { get; set; }

        public float getSalaryByFrecuence(FrequencyofPayments fp)
        {

            return Hourlywages * (int)fp;
        }
    }
}