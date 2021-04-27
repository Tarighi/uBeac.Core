using System;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Firmware : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Processor { get; set; }
        public int ViewOrder { get; set; }
    }
}
