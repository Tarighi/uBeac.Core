using System.Collections.Generic;
using uBeac.Core.Common;

namespace TestApi.Models
{
    public class Unit : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Unit Parent { get; set; }
        public List<Unit> Children { get; set; }
    }
}
