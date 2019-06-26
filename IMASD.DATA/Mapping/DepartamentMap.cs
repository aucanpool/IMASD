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
    public class DepartamentMap: EntityTypeConfiguration<Departament>
    {
        public DepartamentMap()
        {
            ToTable("Departaments");
            HasKey(x=>x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Description).IsOptional().HasMaxLength(150);

            Property(x => x.Active).IsRequired();
        }
    }
}
