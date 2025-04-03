using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Emplooyes;
using CleanArchitecture.Domain.Users;
using GenericRepository;
using Infrastructure.Context;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string ConnectionString = configuration.GetConnectionString("SqlServer")!;     
                options.UseSqlServer(ConnectionString); 
            });

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            services.
                AddIdentity<AppUser, IdentityRole<Guid>>(opt =>
                {
                    opt.Password.RequiredLength = 1;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Lockout.MaxFailedAccessAttempts = 5;  // 5 defa yanlış girerse 5 dk kilitler
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dk kilitler
                    opt.SignIn.RequireConfirmedEmail = true;  // email onayı zorunlu    
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();  // tüm bunlar user manager ve signIn  sınıfını kullanabilmek için

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtOptionsSetup>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();
            services.AddAuthorization();    

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
