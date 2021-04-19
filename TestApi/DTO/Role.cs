using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApi.DTO
{
    public class RoleAddRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleUpdateRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRoleRequest
    {
        public IEnumerable<Guid> RoleIds { get; set; }
        public Guid UserId { get; set; }
    }
}
