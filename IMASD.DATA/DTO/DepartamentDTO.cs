using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMASD.DATA.DTO
{
    public class DepartamentDTO
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}