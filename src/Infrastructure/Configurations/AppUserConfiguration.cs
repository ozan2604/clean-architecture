using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(p => p.UserName).IsUnique();   // usermame i uniq unit üretir
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(50)");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(50)");
            builder.Property(p => p.UserName).HasColumnType("nvarchar(15)");
            builder.Property(p => p.Email).HasColumnType("nvarchar(50)");
        }
    }
}
