using System.Collections.Generic;

namespace uBeac.Core
{
    public class EnumModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EnumValueModel> Values { get; set; }
    }
}
