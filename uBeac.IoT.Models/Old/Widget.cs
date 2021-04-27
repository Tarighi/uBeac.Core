﻿using System;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    public class Widget : BaseEntity
    {
        public string Name { get; set; }
        public Guid DashboardId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public string Setting { get; set; }
    }
}
