using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeGrow.DAL;
using WeGrow.DAL.Interfaces;
using WeGrow.DAL.Repositories;
using WeGrow.Mapper;

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
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var dalAssembly = Assembly.Load("WeGrow.DAL");
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(Assembly.GetExecutingAssembly());
                mc.AddMaps(dalAssembly);
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
