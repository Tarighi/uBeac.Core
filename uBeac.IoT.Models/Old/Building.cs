using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Building : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Coordinate Coordinate { get; set; }
        public Address Address { get; set; }
        public List<Floor> Floors { get; set; }
    }
}
