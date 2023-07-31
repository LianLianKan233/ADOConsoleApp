using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
}