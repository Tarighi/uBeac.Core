using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Sensor : BaseEntity
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public SensorTypes Type { get; set; }
        public SensorUnits Unit { get; set; }
        public string Description { get; set; }
        public SensorPrefixes Prefix { get; set; }
        public List<string> Schema { get; set; }
    }
}
