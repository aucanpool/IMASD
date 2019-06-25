using IMASD.DATA.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace IMASD.DATA.Mapping
{
    public class EmployeeMap: EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("Employees");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasIndex(x => x.JobNumber).IsUnique();
            Property(x => x.JobNumber)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);
            Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            Property(x => x.Address)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);
            Property(x => x.Telefone)
                .IsOptional()
                .HasColumnType("varchar")
                .HasMaxLength(25);
            Property(x => x.Active).IsRequired();
            HasRequired(x => x.Departament)
                .WithMany(d => d.Employees)
                .HasForeignKey(x=>x.DepartamentId);
            HasRequired(x => x.SalaryTabulator)
                .WithMany(d => d.Employees)
                .HasForeignKey(x => x.SalaryTabulatorId);
        }
    }
}
