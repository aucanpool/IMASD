using IMASD.DATA.Entities;
using IMASD.DATA.Mapping;
using IMASD.DATA.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA
{
    public class MainContext : DbContext
    {
        public MainContext(): base("name=BDNOMINA2018")
        {
            Database.SetInitializer<MainContext>(new MigrateDatabaseToLatestVersion<MainContext, Configuration>());
        }
        public virtual DbSet<Departament> Departaments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<SalaryTabulator> SalaryTabulators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DepartamentMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new SalaryTabulatorMap());
        }

    }
}
