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
    public class PaymentMap:  EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            ToTable("Payments");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FrequencyofPayments).IsRequired();
            Property(x => x.StarDate).IsRequired().HasColumnType("datetime");
            Property(x => x.EndDate).IsRequired().HasColumnType("datetime");
            Property(x => x.ProcessedDate).IsRequired().HasColumnType("datetime");
            Property(x => x.VoidDate).IsOptional().HasColumnType("datetime");
            HasRequired(x => x.Employee)
                .WithMany()
                .HasForeignKey(x=>x.EmployeeId);


        }

    }
}
