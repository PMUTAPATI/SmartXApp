using System;
using SmartXApp.Models;

namespace SmartXApp.Services
{
    public class TelemetrySimulator
    {
        private readonly Random rng = new Random();

        public TelemetryData GenerateSensorReading(string deviceId)
        {
            var isSpike = rng.Next(1, 100) <= 15;
            var power = isSpike ? rng.Next(1500, 3000) : rng.Next(100, 600);

            return new TelemetryData
            {
                DeviceId = deviceId,
                DeviceType = "ESP32-HydroNode",
                Timestamp = DateTime.Now,
                SoilMoisture = (float)Math.Round(rng.NextDouble() * 100, ),
                PowerWattage = power,
                ValveState = rng.Next(0, 2) == 1,
                IsConnected = true
            };
        }
    }
}