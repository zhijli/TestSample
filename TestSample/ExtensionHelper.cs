using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    public static class ExtensionHelper
    {
        public static string EnumString(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (TestSample.EnumSample.EnumStringAttribute[])fi.GetCustomAttributes(typeof(TestSample.EnumSample.EnumStringAttribute), false);
            return (attributes.Length > 0) ? attributes[0].str : value.ToString();
        }

        public static Enum WrongRandomItem(this Enum value)
        {
            Type type = value.GetType();
            var random = new Random(DateTime.Now.Millisecond);
            var i = random.Next(0, type.GetEnumNames().Length);
            return (Enum)Enum.Parse(type, i.ToString());
        }

        public static Enum BadRandomItem(this Enum value)
        {
            Type type = value.GetType();
            var random = new Random(DateTime.Now.Millisecond);
            var i = random.Next(0, type.GetEnumNames().Length);
            string[] names = Enum.GetNames(type);
            return (Enum)Enum.Parse(type, names[i]);
        }
        
        public static Enum GoodRandomItem(this Enum value)
        {
            Type type = value.GetType();
            var random = new Random(DateTime.Now.Millisecond);
            var i = random.Next(0, type.GetEnumNames().Length);
            return (Enum) type.GetEnumValues().GetValue(i);
        }

        public static T BetterRandomItem<T>(this T value)
        {
            if (!(value is Enum))
            {
                return default(T);
            }
            var type = value.GetType();
            var random = new Random(DateTime.Now.Millisecond);
            var i = random.Next(0, type.GetEnumValues().Length);
            return (T)type.GetEnumValues().GetValue(i);
        }

        public static Enum AnotherRandomItem(this Type type)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var i = random.Next(0, type.GetEnumValues().Length);
            return (Enum)type.GetEnumValues().GetValue(i);
        }


    }
}
