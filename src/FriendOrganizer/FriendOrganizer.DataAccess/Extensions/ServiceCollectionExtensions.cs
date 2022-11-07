using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FriendOrganizer.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FriendOrganizerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(FriendOrganizerDbContext)));
            });

            return services;
        }
    }
}
