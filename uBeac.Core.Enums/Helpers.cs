using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace uBeac.Core
{
    public static class Helpers
    {
        public static IEnumerable<EnumModel> GetEnums(this Assembly assembly) 
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsEnum) continue;

                var enumAttributes = type.GetCustomAttributes<EnumAttribute>(true).ToList();

                if (enumAttributes.Count == 0) continue;

                var enumModel = new EnumModel
                {
                    Name = type.Name,
                    Description = string.Join("", enumAttributes.Where(x => !string.IsNullOrEmpty(x.Description)).Select(x => x.Description)),
                    Values = new List<EnumValueModel>()
                };


                foreach (object enumValue in Enum.GetValues(type))
                {
                    var fieldInfo = type.GetField(enumValue.ToString());
                    var fieldAttributes = fieldInfo.GetCustomAttributes<DescriptionAttribute>(false);
                    enumModel.Values.Add(new EnumValueModel
                    {
                        Value = enumValue,
                        Name = Enum.GetName(type, enumValue),
                        Description = string.Join("", fieldAttributes.Select(x => x.Description))
                    });
                }
                yield return enumModel;
            }
        }
        public static IEnumerable<EnumModel> GetEnums(this IEnumerable<AssemblyName> assemblyNames) 
        {
            foreach (var assemblyName in assemblyNames)
            {
                Assembly assembly = Assembly.Load(assemblyName);

                foreach (var enumModel in assembly.GetEnums())
                {
                    yield return enumModel;
                }
            }
        }

    }
}
