using Microsoft.EntityFrameworkCore;
using WeGrow.DAL;
using WeGrow.DAL.Interfaces;
using WeGrow.DAL.Repositories;

namespace WeGrow.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IRepository, Repository>();
        }
    }
}
