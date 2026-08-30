using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using SmartXApp.Models;
using SmartXApp.Services;

namespace SmartXApp.ViewModels
{
    public class MainViewModel
    {
        private readonly TelemetryService telemetryService = new TelemetryService();
        private readonly TelemetrySimulator simulator = new TelemetrySimulator();
        private readonly DispatcherTimer timer;
        private int nodeCounter = 1;

        public ObservableCollection<TelemetryData> TelemetryStream { get; set; } = new ObservableCollection<TelemetryData>();

        public MainViewModel()
        {
            GenerateNewReading("ESP32-Node-01");
            GenerateNewReading("ESP32-Node-02");

            // Reference: Microsoft Docs - DispatcherTimer Class for UI Thread Updates
            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            string deviceId = $"ESP32-Node-0{nodeCounter}";
            GenerateNewReading(deviceId);

            nodeCounter = nodeCounter == 1 ? 2 : 1;
        }

        public void GenerateNewReading(string deviceId)
        {
            var reading = simulator.GenerateSensorReading(deviceId);
            telemetryService.ValidateTelemetry(reading);

            if (TelemetryStream.Count >= 15)
            {
                TelemetryStream.RemoveAt(0);
            }

            TelemetryStream.Add(reading);
        }
    }
}