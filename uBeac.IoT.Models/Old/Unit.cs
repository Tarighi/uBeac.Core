using System;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
