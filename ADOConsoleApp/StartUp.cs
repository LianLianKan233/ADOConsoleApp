using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.Versioning;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAdoExecutor, AdoExecutor>((serviceProvider) =>
        {
            return new AdoExecutor("O365%20Core", "xhxmpqprdyagfcyo5eqq7haxuxlhvym2bvd6ruuh2pzavy5daxbq");
        });
    }

    [SupportedOSPlatform("windows")]
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // In Development, use the Developer Exception Page
            app.UseDeveloperExceptionPage();
            app.UseRouting();
        }
        else
        {
            // In Staging/Production, route exceptions to /error
            app.UseExceptionHandler("/error");
            app.UseRouting();
        }
    }
}