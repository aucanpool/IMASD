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
    public class JobMap : EntityTypeConfiguration<Job>
    {
        public JobMap()
        {
            ToTable("Jobs")
                .HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Key)
                .IsUnicode()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            Property(x=>x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            Property(x => x.Active).IsRequired();
        }
    }
}
