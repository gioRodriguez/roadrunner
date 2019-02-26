using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Roadrunner.DriverClient.Models;

namespace Roadrunner.DriverClient
{
    public class RoadRunnerDriverClient
    {
        private readonly string _roadrunnerUrl;
        private readonly string _driverId;
        private readonly string _driverApiKey;
        private readonly Func<TripRequest, Task<TripRequestAnswer>> _receivedTripRequest;
        private HubConnection _connection;                

        private RoadRunnerDriverClient(string roadrunnerUrl, string driverId, string driverApiKey, Func<TripRequest, Task<TripRequestAnswer>> receivedTripRequest)
        {
            _roadrunnerUrl = roadrunnerUrl;
            _driverId = driverId;
            _driverApiKey = driverApiKey;
            _receivedTripRequest = receivedTripRequest;
        }

        public async Task StartClient(int x, int y)
        {
            _connection = CreateHubConnection();
            RegisterIncominEvents(_connection);
            await _connection.StartAsync();
            await _connection.InvokeAsync("DriverReadyAtPosition", new PositionModel { X = x, Y = y });
        }

        private void RegisterIncominEvents(HubConnection conn)
        {
            conn.On<string>("NewTripRequest", async passengerId =>
            {
                Console.WriteLine($"Trip request received by {_driverId} issue by {passengerId}");

                var answer = await _receivedTripRequest(new TripRequest { PassengerId = passengerId });
                if (answer.Answer == TripRequestAnswer.TripRequestAnswers.Accepted)
                {
                    await conn.InvokeAsync("DriverTripAccepted", passengerId);
                }
            });
        }

        private HubConnection CreateHubConnection()
        {
            var accessToken = $"{_driverId}:{Signature(_driverId, Encoding.UTF8.GetBytes(_driverApiKey))}";
            var conn = new HubConnectionBuilder()
                .WithUrl($"{_roadrunnerUrl}/driversHub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                })
                .Build();

            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
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

        public async Task ReportMovementAsync(int x, int y)
        {
            await _connection.InvokeAsync("DriverPositionUpdate", new PositionModel { X = x, Y = y });
        }

        public class Builder
        {
            private readonly string _driverId;
            private readonly string _driverApiKey;
            private readonly string _roadrunnerUrl;
            private Func<TripRequest, Task<TripRequestAnswer>> _receivedTripRequest;

            public Builder(string driverId, string driverApiKey, string roadrunnerUrl)
            {
                _driverId = driverId;
                _driverApiKey = driverApiKey;
                _roadrunnerUrl = roadrunnerUrl;
            }

            public Builder OnReceivedTripRequest(Func<TripRequest, Task<TripRequestAnswer>> receivedTripRequest)
            {
                _receivedTripRequest = receivedTripRequest;
                return this;
            }

            public RoadRunnerDriverClient Build()
            {
                return new RoadRunnerDriverClient(_roadrunnerUrl, _driverId, _driverApiKey, _receivedTripRequest);
            }
        }
        
    }
}
