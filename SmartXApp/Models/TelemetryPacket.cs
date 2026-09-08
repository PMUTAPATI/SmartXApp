using System;

namespace SmartXApp.Models
{
    public class TelemetryPacket<T>
    {
        public string PacketId { get; set; } = Guid.NewGuid().ToString("N");
        public string DeviceMac { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public T Value { get; set; }

        public TelemetryPacket(string deviceMac, T value)
        {
            DeviceMac = deviceMac;
            Value = value;
            Value
        }
    }
}