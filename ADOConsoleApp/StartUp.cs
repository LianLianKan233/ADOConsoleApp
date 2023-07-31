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
        services.AddScoped<IQueryExecutor, QueryExecutor>((serviceProvider) =>
        {
            return new QueryExecutor("orgName", "accessToken");
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

        var contentRootPath = env.ContentRootPath;
    }
}