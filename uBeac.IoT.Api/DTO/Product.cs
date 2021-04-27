using System;
using System.Collections.Generic;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class ProductAddRequestDTO
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ViewOrder { get; set; }
        public Guid Image { get; set; }
        public string Url { get; set; }
    }

    public class ProductUpdateRequestDTO : ProductAddRequestDTO
    {
        public Guid Id { get; set; }
    }

    public class ProductViewDTO : Product
    {
        public List<FirmwareViewDTO> Firmwares { get; set; }
    }
}
