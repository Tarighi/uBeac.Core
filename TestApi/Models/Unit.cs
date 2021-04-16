using System;
using System.Collections.Generic;
using uBeac.Core.Common;

namespace TestApi.Models
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? Parent { get; set; }
        public List<Guid> Children { get; set; }
    }
}
