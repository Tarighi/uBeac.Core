using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
