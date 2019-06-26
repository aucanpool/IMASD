namespace IMASD.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "JobNumber" });
            DropIndex("dbo.Salary_tabulators", new[] { "Key" });
            DropIndex("dbo.Jobs", new[] { "Key" });
            AddColumn("dbo.Departaments", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Salary_tabulators", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Active");
            DropColumn("dbo.Salary_tabulators", "Active");
            DropColumn("dbo.Departaments", "Active");
            CreateIndex("dbo.Jobs", "Key", unique: true);
            CreateIndex("dbo.Salary_tabulators", "Key", unique: true);
            CreateIndex("dbo.Employees", "JobNumber", unique: true);
        }
    }
}
