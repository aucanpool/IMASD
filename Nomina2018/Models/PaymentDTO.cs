using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nomina2018.Models
{
    public class PaymentDTO
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }

        [Display(Name = "Frecuencia de pagos")]
        public FrequencyofPayments FrequencyofPayments { get; set; }

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "Requerido.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StarDate { get; set; }

        [Display(Name = "Fecha de termino")]
        [Required(ErrorMessage = "Requerido.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Día procesado")]
        [Required(ErrorMessage = "Requerido.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProcessedDate { get; set; }

        [Display(Name = "Fecha de cancelación")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? VoidDate { get; set; }

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "Requerido.")]
        public int EmployeeId { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
    }
}