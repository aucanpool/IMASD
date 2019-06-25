using IMASD.DATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMASD.DATA.Mapping
{
    public class SalaryTabulatorMap:EntityTypeConfiguration<SalaryTabulator>
    {
        public SalaryTabulatorMap()
        {
            ToTable("Salary_tabulators");
            HasKey(x=>x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            

            HasIndex(x => x.Key).IsUnique();
            Property(x => x.Key).IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            // Foreign key Job
            HasRequired(x => x.Job)
                .WithMany(e=>e.SalaryTabulators)
                .HasForeignKey(x=>x.JobId)
                .WillCascadeOnDelete(false);
            Property(x => x.TabulatorLevel).HasColumnName("Level").IsRequired();

        }
    }
}
