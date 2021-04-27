using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class ManufacturerAddRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid Website!")]
        [Required]
        public string Website { get; set; }
        public Guid Logo { get; set; }
    }
    public class ManufacturerUpdateRequest : ManufacturerAddRequest
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class ManufacturerResponse : Manufacturer
    {
        public List<ProductResponse> Products { get; set; }
    }

}
