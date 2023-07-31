using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Runtime.Versioning;

class Program
{
    /// <summary>
    /// Main entry point
    /// </summary>
    /// <param name="args">Arguments list</param>
    [SupportedOSPlatform("windows")]
    public static void Main(string[] args)
    {
        IWebHost webHost = BuildWebHost(args);
        try
        {
            webHost.Run();
        }
        catch (HttpSysException ex)
        {
            Environment.Exit(ex.ErrorCode);
        }

        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        //await QueryExecutor.PrintOpenBugsAsync("ess");

        while (!endApp)
        {
            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            var numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
        }
    }

    /// <summary>
    /// Method to build web host
    /// </summary>
    /// <param name="args">Argument list</param>
    /// <returns>web host</returns>
    [SupportedOSPlatform("windows")]
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseHttpSys(o =>
            {
                o.AllowSynchronousIO = true;
            })
        .UseUrls("https://+:444/fhl/")
        .UseStartup<Startup>()
        .Build();
}