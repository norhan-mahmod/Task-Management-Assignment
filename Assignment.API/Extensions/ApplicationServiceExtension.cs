using Assignment.API.Mapping;
using Assignment.Core.RepoInterfaces;
using Assignment.Core.ServiceInterfaces;
using Assignment.Repository.Repositories;
using Assignment.Service;

namespace Assignment.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaskModelRepository, TaskModelRepository>();
            services.AddScoped<ITaskService , TaskService>();
            services.AddScoped<ITaskProcessor , TaskProcessor>();
            services.AddScoped<ICachService , CachService>();
            services.AddAutoMapper(m => m.AddProfile<MappingProfiles>());
            return services;
        }
    }
}
