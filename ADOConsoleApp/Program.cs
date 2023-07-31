using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
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
        return;
    }
}