using System;
using System.Collections.Generic;
using uBeac.IoT.Models;

namespace uBeac.IoT.Api.DTO
{
    public class ManufacturerAddRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public Guid Logo { get; set; }
    }
    public class ManufacturerUpdateRequest : ManufacturerAddRequest
    {
        public Guid Id { get; set; }
    }

    public class ManufacturerResponse : Manufacturer
    {
        public List<ProductResponse> Products { get; set; }
    }

}
