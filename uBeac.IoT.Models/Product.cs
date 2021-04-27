using System;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Product : BaseEntity
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ViewOrder { get; set; }
        public Guid Image { get; set; }
        public string Url { get; set; }
    }
}
