using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Migrations
{
    internal sealed class Configuration: DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(MainContext context)
        {

            if (!context.Departaments.Any())
            {
                var departaments = getDepartaments(3);
                context.Departaments.AddOrUpdate(departaments.ToArray());
                context.SaveChanges();
            }
            if (!context.Jobs.Any())
            {
                var jobs = getJobs(3);
                context.Jobs.AddOrUpdate(jobs.ToArray());
                context.SaveChanges();
            }
            if (!context.SalaryTabulators.Any())
            {
                var salaryTabulators = getSalaryTabulator();
                context.SalaryTabulators.AddOrUpdate(salaryTabulators.ToArray());
                context.SaveChanges();
            }
            if (!context.Employees.Any())
            {
                var employees = getEmployee();
                context.Employees.AddOrUpdate(employees.ToArray());
                context.SaveChanges();
            }

            
        }

        private List<Departament> getDepartaments(int number)
        {
            var departamentos = new List<Departament>();
            for (int i = 0; i < number; i++)
            {
                departamentos.Add(new Departament() {Name="Departamento "+i, Description="Departamento description"+i ,Active=true});
            }
            return departamentos;

        }
        private List<Job> getJobs(int number)
        {
            var jobs = new List<Job>();
            for (int i = 0; i < number; i++)
            {
                jobs.Add(new Job() { Name = "Trabajo " + i, Key = "EED00" + i , Active=true});
            }
            return jobs;
        }
        private List<SalaryTabulator> getSalaryTabulator()
        {
            var salaryTabulators = new List<SalaryTabulator>();
            salaryTabulators.Add(new SalaryTabulator()
            { Key = "SALTAB001",
                JobId = 1,
                TabulatorLevel = Base.ENUMS.TabulatorLevel.A,
                Hourlywages = 150,
                AnnualBonusDays = 15,
                AnnualVacationDays=6,
                Active = true
            }
            );
            salaryTabulators.Add(new SalaryTabulator()
            {
                Key = "SALTAB002",
                JobId = 1,
                TabulatorLevel = Base.ENUMS.TabulatorLevel.B,
                Hourlywages = 250,
                AnnualBonusDays = 18,
                AnnualVacationDays = 8,
                Active = true
            });
            salaryTabulators.Add(new SalaryTabulator()
            {
                Key = "SALTAB003",
                JobId = 1,
                TabulatorLevel = Base.ENUMS.TabulatorLevel.C,
                Hourlywages = 450,
                AnnualBonusDays = 25,
                AnnualVacationDays = 15,
                Active=true
            });

            return salaryTabulators;
        }
        private List<Employee> getEmployee()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee()
            {
                JobNumber = "92393",
                FirstName = "Angel Alfredo",
                LastName = "Ucan Pool",
                Address = "Calle 18 num 130 por 29 y 31 Col. Timucuy",
                Telefone = "9991-48-18-94",
                Gender = Base.ENUMS.Gender.Male,
                HireDate= DateTime.Now,
                Active=true,
                DepartamentId=1,
                SalaryTabulatorId=1
            }
            );
            employees.Add(new Employee()
            {
                JobNumber = "92394",
                FirstName = "Luis Alberto",
                LastName = "Uk Pech",
                Address = "Calle 120 num 30  Col. Kanasin",
                Telefone = "9991-23-45-67",
                Gender = Base.ENUMS.Gender.Male,
                HireDate = DateTime.Now,
                Active = true,
                DepartamentId = 2,
                SalaryTabulatorId = 2
            }
            );
            employees.Add(new Employee()
            {
                JobNumber = "92395",
                FirstName = "Adrian Cielo",
                LastName = "Lopez Camal",
                Address = "Calle 54 num 12  Col. Hunucma",
                Telefone = "9991-23-45-67",
                Gender = Base.ENUMS.Gender.Female,
                HireDate = DateTime.Now,
                Active = true,
                DepartamentId = 3,
                SalaryTabulatorId = 3
            }
            );
            employees.Add(new Employee()
            {
                JobNumber = "92396",
                FirstName = "Yuritzi Rosalia",
                LastName = "Chan Flores",
                Address = "Calle 20 num 30 entre 89 y 91  Col. Tizimin",
                Telefone = "9991-44-35-29",
                Gender = Base.ENUMS.Gender.Female,
                HireDate = DateTime.Now,
                Active = true,
                DepartamentId = 2,
                SalaryTabulatorId = 3
            }
            );

            return employees;
        }


    }
}
