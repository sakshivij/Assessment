using Microsoft.Extensions.DependencyInjection;

namespace CountryGwp.Api.Web
{
    public static class WebApplicationMvcBuilderExtension
    {
        public static IMvcBuilder AddWebApplicationControllers(this IMvcBuilder builder)
        {
            return builder
                .AddApplicationPart(typeof(WebApplicationMvcBuilderExtension).Assembly)
                .AddControllersAsServices();
        }
    }
}
