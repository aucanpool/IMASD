namespace IMASD.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 150),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobNumber = c.String(nullable: false, maxLength: 50, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 100, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        Telefone = c.String(maxLength: 25, unicode: false),
                        Gender = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DepartamentId = c.Int(nullable: false),
                        SalaryTabulatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departaments", t => t.DepartamentId, cascadeDelete: true)
                .ForeignKey("dbo.Salary_tabulators", t => t.SalaryTabulatorId, cascadeDelete: true)
                .Index(t => t.DepartamentId)
                .Index(t => t.SalaryTabulatorId);
            
            CreateTable(
                "dbo.Salary_tabulators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100, unicode: false),
                        JobId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Hourlywages = c.Single(nullable: false),
                        AnnualHolidayBonus = c.Single(nullable: false),
                        AnnualBonusDays = c.Int(nullable: false),
                        AnnualVacationDays = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 50, unicode: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FrequencyofPayments = c.Int(nullable: false),
                        StarDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ProcessedDate = c.DateTime(nullable: false),
                        VoidDate = c.DateTime(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "SalaryTabulatorId", "dbo.Salary_tabulators");
            DropForeignKey("dbo.Salary_tabulators", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Employees", "DepartamentId", "dbo.Departaments");
            DropIndex("dbo.Payments", new[] { "EmployeeId" });
            DropIndex("dbo.Salary_tabulators", new[] { "JobId" });
            DropIndex("dbo.Employees", new[] { "SalaryTabulatorId" });
            DropIndex("dbo.Employees", new[] { "DepartamentId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Jobs");
            DropTable("dbo.Salary_tabulators");
            DropTable("dbo.Employees");
            DropTable("dbo.Departaments");
        }
    }
}
