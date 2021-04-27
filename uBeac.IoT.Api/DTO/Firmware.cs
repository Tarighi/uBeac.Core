using System;
using System.ComponentModel.DataAnnotations;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class FirmwareAddRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int ViewOrder { get; set; }
    }

    public class FirmwareUpdateRequest: FirmwareAddRequest
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class FirmwareResponse : Firmware
    {
    }
}
