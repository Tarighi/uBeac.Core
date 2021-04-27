using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class TeamAddRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string Namespace { get; set; }

        public Dictionary<string, object> Attributes { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public Address Address { get; set; }
    }
    public class TeamUpdateRequest : TeamAddRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
    public class TeamResponse : Team
    { 
    }
}
