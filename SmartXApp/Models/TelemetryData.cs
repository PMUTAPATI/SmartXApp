using System;

namespace SmartXApp.Models
{
    public class TelemetryData
    {
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        
        // Sensor readings
        public float SoilMoisture { get; set; }
        public int PowerWattage { get; set; }
        public bool ValveState { get; set; }
       
        // Status indicators
        public bool IsAnomaly { get; set; }
        public bool IsConnected { get; set; } = true;
        public string AlertMessage { get; set; } = string.Empty;
    }
}