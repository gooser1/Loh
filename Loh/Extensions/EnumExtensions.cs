using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Loh.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var attr = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>();

            if (attr is null)
                return "";

            return attr.Description;
        }

        public static bool In(this System.Enum someenum, params System.Enum[] values)
        {
            return values.Contains(someenum);
        }
    }
}
