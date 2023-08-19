using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CountryGwp.Api.MySQL;
using CountryGwp.Api.Web;
using System;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using Google.Protobuf.WellKnownTypes;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddMvc( config =>
            {
                config.EnableEndpointRouting = false;
            })
            .AddWebApplicationControllers();
        services.AddControllers();
        services.AddHttpClient();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v2", new OpenApiInfo 
            { 
                Title = "CountryGwp", Version = "v1" ,
                Description = "An ASP.NET Core Web API for getting average GWP",
                TermsOfService = new Uri("https://example.com/terms"),
            });
            foreach (var filePath in System.IO.Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!), "*.xml"))
            {
                try
                {
                    c.IncludeXmlComments(filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        });

       
        services.AddMySQLDataServices();
    } 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "CountryGwp");
        });
        app.UseMvc();
    }
}
