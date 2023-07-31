using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Run();
    }

    public static WebApplication CreateHostBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddScoped<IQueryExecutor, QueryExecutor>((serviceProvider) =>
        {
            return new QueryExecutor("orgName", "accessToken");
        });
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
