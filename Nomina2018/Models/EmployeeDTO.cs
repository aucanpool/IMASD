using IMASD.Base.ENUMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Nomina2018.Models
{
    public class EmployeeDTO
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }
        [Display(Name = "Numero de empleado")]
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(50)]
        public string JobNumber { get; set; }
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(50)]
        public String LastName { get; set; }
        [Display(Name = "Dirección")]

        [Required(ErrorMessage = "Requerido.")]
        [StringLength(150)]
        public String Address { get; set; }

        [Display(Name = "Télefono")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Formato invalido")]
        public string Telefone { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "Requerido.")]
        public Gender Gender { get; set; }

        [Display(Name = "Fecha de contratación")]
        [Required(ErrorMessage = "Requerido.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        // Departament

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Requerido.")]
        public int DepartamentId { get; set; }
        public virtual DepartamentDTO Departament { get; set; }

        //Salary Tabulator

        [Display(Name = "Tabulador Salarial")]
        [Required(ErrorMessage = "Requerido.")]
        public int SalaryTabulatorId { get; set; }
        public virtual SalaryTabulatorDTO SalaryTabulator { get; set; }

        public string getFullName()
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append(FirstName).Append(" ").Append(LastName).ToString();
        }

    }
}