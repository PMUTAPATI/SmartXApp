using System;
using SmartXApp.Models;

namespace SmartXApp.Services
{
    /// Handles validation and anomaly detection for incoming telemetry data.
    public class TelemetryService
    {
        public void ValidateTelemetry(TelemetryData data)
        {
            if (data == null) return;

            data.IsAnomaly = false;
            data.AlertMessage = "Normal";

            if (data.PowerWattage > 1200)
            {
                data.IsAnomaly = true;
                data.AlertMessage = $"High power usage: {data.PowerWattage}W";
            }
            else if (data.SoilMoisture < 10.0f || data.SoilMoisture > 90.0f)
            {
                data.IsAnomaly = true;
                data.AlertMessage = $"Moisture out of bounds: {data.SoilMoisture}";
            }
        }
    }
}