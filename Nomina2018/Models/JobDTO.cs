using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nomina2018.Models
{
    public class JobDTO
    {
        [Display(Name = "Idetificador")]
        public int Id { get;  set; }
        [Display(Name = "Clave")]
        [StringLength(50)]
        public string Key { get; set; }
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<SalaryTabulatorDTO> SalaryTabulators { get; set; }
    }
}