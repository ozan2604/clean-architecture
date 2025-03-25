using CleanArchitecture.Domain.Emplooyes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsOne(p => p.PersonelInformation, builder =>
            {
                builder.Property(i => i.IdentityNumber).HasColumnName("IdentityNumber");
                builder.Property(i => i.Phone1).HasColumnName("Phone1");
                builder.Property(i => i.Phone2).HasColumnName("Phone2");
                builder.Property(i => i.Email).HasColumnName("Email");
            });

            builder.OwnsOne(p => p.Address, builder =>
            {
                builder.Property(i => i.Country).HasColumnName("Country");
                builder.Property(i => i.City).HasColumnName("City");
                builder.Property(i => i.Town).HasColumnName("Town");
                builder.Property(i => i.FulLAddress).HasColumnName("FulLAddress");
            });

            builder.Property(p => p.Salary).HasColumnType("money");
        }
    }
}
