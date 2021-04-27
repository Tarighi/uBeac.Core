using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Description { get; set; }
    }
}