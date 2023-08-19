using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Api.MySQL
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddMySQLDataServices(this IServiceCollection services)
        {
            return services.AddTransient<ICountryGwpDataService, CountryGwpDataService>();
        }
    }
}
