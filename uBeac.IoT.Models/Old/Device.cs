using System;
using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Device : BaseEntity
    {
        public Guid FirmwareId { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? FloorId { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public long RequestCount { get; set; }
        public Coordinate RelativeCoordinate { get; set; }
        public Coordinate Coordinate { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}
