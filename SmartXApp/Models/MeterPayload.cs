namespace SmartXApp.Models
{
    public class MeterPayload
    {
        public string DeviceMac { get; set; } = string.Empty;
        public double PowerUsageKw { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public MeterPayload() { }

        public MeterPayload(double powerUsageKw)
        {
            PowerUsageKw = powerUsageKw;
        }

        // Binary '+' operator overload to sum power usage across meters
        public static MeterPayload operator +(MeterPayload a, MeterPayload b)
        {
            return new MeterPayload
            {
                PowerUsageKw = (a?.PowerUsageKw ?? 0) + (b?.PowerUsageKw ?? 0)
            };
        }
    }
}
// Reference: Microsoft Docs - C# Operator Overloading
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading