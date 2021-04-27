using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class ProductAddRequest
    {
        [Required]
        public Guid ManufacturerId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int ViewOrder { get; set; }
        public Guid Image { get; set; }

        [Url(ErrorMessage = "Invalid URL!")]
        public string Url { get; set; }
    }

    public class ProductUpdateRequest : ProductAddRequest
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class ProductResponse : Product
    {
        public List<FirmwareResponse> Firmwares { get; set; }
    }
}
