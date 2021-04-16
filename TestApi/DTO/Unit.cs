﻿using System;
using System.Collections.Generic;
using uBeac.Core.Common;

namespace TestApi.DTO
{
    public class UnitAddRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class UnitUpdateRequestDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UnitUpdateParentRequestDTO
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
    }
    public class UnitViewDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UnitViewDTO> Children { get; set; }
    }
}
