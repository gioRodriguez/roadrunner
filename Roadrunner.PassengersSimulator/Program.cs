using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Roadrunner.PassengersSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var random = new Random();
                Console.WriteLine("Please wait till the server is ready, then press enter");
                Console.ReadLine();

                var passengerId = $"passenger-{random.Next(0, 1000)}";
                var hubConn = StartClient(passengerId).Result;
                Console.WriteLine("To request trips press <space>");

                while (true)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        var origin = new PositionModel {X = random.Next(0, 100), Y = random.Next(0, 100)};
                        var destiny = new PositionModel {X = random.Next(0, 100), Y = random.Next(0, 100)};
                        Console.WriteLine($"\n\n{passengerId} has asked a new trip from origin: {origin.X}, {origin.Y} with destiny: {destiny.X}, {destiny.Y}");
                        hubConn.InvokeAsync("TripRequest", origin, destiny);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

        public static async Task<HubConnection> StartClient(string passengerId)
        {
            var _connection = CreateHubConnection(passengerId);
            RegisterIncominEvents(_connection);
            await _connection.StartAsync();
            return _connection;
        }

        private static HubConnection CreateHubConnection(string passengerId)
        {
            var accessToken = $"{passengerId}:{Signature(passengerId, Encoding.UTF8.GetBytes(passengerId))}";
            var conn = new HubConnectionBuilder()
                .WithUrl("http://localhost:49844/passengersHub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                })
                .Build();

            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };

            return conn;
        }

        private static string Signature(string url, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(url)));
            }
        }

        private static void RegisterIncominEvents(HubConnection conn)
        {
            conn.On<string>("DriverAssigned", driverId =>
            {
                Console.WriteLine($"Driver assigned: {driverId}");
            });
        }

        internal class PositionModel
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
