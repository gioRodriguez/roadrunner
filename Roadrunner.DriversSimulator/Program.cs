using System;
using System.Threading;

namespace Roadrunner.DriversSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many drivers would you like to simulate?");
            var drivers = Console.ReadLine();
            int.TryParse(drivers, out var driversToSimulate);

            var driversSimulator = new DriverSimulator.Builder()
                .NumberOfDrivers(driversToSimulate)
                .RegionSize(1000, 1000)
                .MovementTimeSpan(TimeSpan.FromSeconds(1))
                .Build();

            var ct = new CancellationTokenSource();
            var simulationTask = driversSimulator.StartSimulationAsync(ct.Token);

            try
            {
                while (true)
                {
                    Console.WriteLine("To end simulation please type <ESC>");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        ct.Cancel();
                        Console.WriteLine("Ending...");
                        simulationTask.Wait(CancellationToken.None);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }            
        }
    }
}
