using System.Collections.Generic;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Dashboard : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double ViewOrder { get; set; }
        public List<Widget> Widgets { get; set; }
    }
}
