using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Gateway> Gateways { get; set; }
        public List<Device> Devices { get; set; }
        public List<Dashboard> Dashboards { get; set; }
    }
}