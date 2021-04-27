//using System;
//using System.Collections.Generic;
//using uBeac.Core;
//using uBeac.IoT.Models;

//namespace uBeac.IoT.Api.DTO
//{
//    public class UnitAddRequestDTO
//    {
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public Guid? ParentId { get; set; }
//    }

//    public class UnitUpdateRequestDTO
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//    }

//    public class UnitUpdateParentRequestDTO 
//    {
//        public Guid Id { get; set; }
//        public Guid? ParentId { get; set; }
//    }

//    public class UnitViewDTO : BaseEntity
//    {
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public List<Unit> Children { get; set; }
//    }

//}
