using System;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Floor : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PlanFileId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
