using IMASD.Base.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string JobNumber { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public string Telefone { get; set; }
        public Gender Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool Active { get; set; }

        // Departament
        public int DepartamentId { get; set; }
        public virtual Departament Departament { get; set; }

        //Salary Tabulator
        public int SalaryTabulatorId { get; set; }
        public virtual SalaryTabulator SalaryTabulator { get; set; }
        
    }
}
