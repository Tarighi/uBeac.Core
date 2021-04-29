using System;

namespace uBeac.Core
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumAttribute : Attribute
    {
        public string Description { get; set; }
        public EnumAttribute(string description)
        {
            Description = description;
        }
        public EnumAttribute()
        {

        }
    }
}
