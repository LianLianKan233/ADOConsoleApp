﻿using Microsoft.AspNetCore.Builder;
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
        builder.Services.AddScoped<IAdoExecutor, AdoExecutor>((serviceProvider) =>
        {
            return new AdoExecutor("O365%20Core", "xhxmpqprdyagfcyo5eqq7haxuxlhvym2bvd6ruuh2pzavy5daxbq");
        });
        builder.Services.AddScoped<ITeamsExecutor, TeamsExecutor>((serviceProvider) =>
        {
            return new TeamsExecutor();
        });
        builder.Services.AddScoped<ISKExecutor, SK>((serviceProvider) =>
        {
            return new SK();
        });
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
