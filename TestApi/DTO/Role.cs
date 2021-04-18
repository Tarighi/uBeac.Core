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


}
