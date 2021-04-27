using System;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class FirmwareAddRequestDTO
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ViewOrder { get; set; }
    }

    public class FirmwareUpdateRequestDTO: FirmwareAddRequestDTO
    {
        public Guid Id { get; set; }
    }

    public class FirmwareViewDTO : Firmware
    {
    }
}
