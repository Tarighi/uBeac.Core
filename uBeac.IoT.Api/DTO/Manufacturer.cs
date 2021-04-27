using System;
using System.Collections.Generic;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class ManufacturerAddRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public Guid Logo { get; set; }
    }
    public class ManufacturerUpdateRequestDTO : ManufacturerAddRequestDTO
    {
        public Guid Id { get; set; }
    }

    public class ManufacturerViewDTO : Manufacturer
    {
        public List<ProductViewDTO> Products { get; set; }
    }

}
