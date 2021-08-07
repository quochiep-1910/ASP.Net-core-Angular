using API.Data;
using API.Data.Repository;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.IRepository;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //config
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            //Add DI
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IPhotoService,PhotoService>();

            //Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

              //connect Database
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatingWebDb")));

            return services;
        }
    }
}