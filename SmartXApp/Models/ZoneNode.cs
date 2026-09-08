using System;
using System.Collections.Generic;

namespace SmartXApp.Models
{
    public class ZoneNode
    {
        public string ZoneName { get; set; } = string.Empty;
        public List<ZoneNode> SubZones { get; set; } = new List<ZoneNode>();

        public bool ValidateZoneRecursive(string targetZoneName)
        {
            if (this.ZoneName.Equals(targetZoneName, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var child in SubZones)
            {
                if (child.ValidateZoneRecursive(targetZoneName))
                    return true;
            }

            return false;fa
        }
    }
}