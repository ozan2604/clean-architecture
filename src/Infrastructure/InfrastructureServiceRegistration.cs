using CleanArchitecture.Domain.Emplooyes;
using GenericRepository;
using Infrastructure.Context;
using Infrastructure.Repositories;
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

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
