using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Emplooyes;
using CleanArchitecture.Domain.Users;
using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser , IdentityRole<Guid> , Guid> , IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Ignore<IdentityUserClaim<Guid>>();
            modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
            modelBuilder.Ignore<IdentityUserToken<Guid>>();
            modelBuilder.Ignore<IdentityUserLogin<Guid>>();
            modelBuilder.Ignore<IdentityUserRole<Guid>>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Entity>();

            Guid? userId = null;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var parsedUserId))
            {
                userId = parsedUserId;
            }

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreateAt).CurrentValue = DateTimeOffset.Now;
                    if (userId.HasValue)
                        entry.Property(p => p.CreateUserId).CurrentValue = userId.Value;
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Property(p => p.IsDeleted).CurrentValue is true)
                    {
                        entry.Property(p => p.DeleteAt).CurrentValue = DateTimeOffset.Now;
                        if (userId.HasValue)
                            entry.Property(p => p.DeleteUserId).CurrentValue = userId.Value;
                    }
                    else
                    {
                        entry.Property(p => p.UpdateAt).CurrentValue = DateTimeOffset.Now;
                        if (userId.HasValue)
                            entry.Property(p => p.UpdateUserId).CurrentValue = userId.Value;
                    }
                }

                if (entry.State == EntityState.Deleted)
                {
                    throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }



    }
}
