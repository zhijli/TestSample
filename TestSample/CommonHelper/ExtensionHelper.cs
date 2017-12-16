using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

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

        public static string ReportAllProperties<T>(this T instance) where T : class
        {

            if (instance == null)
                return string.Empty;

            var strListType = typeof(List<string>);
            var strArrType = typeof(string[]);

            var arrayTypes = new[] { strListType, strArrType };
            var handledTypes = new[] { typeof(bool), typeof(Int32), typeof(String), typeof(DateTime), typeof(double), typeof(decimal), strListType, strArrType };

            var validProperties = instance.GetType()
                                          .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                          .Where(prop => handledTypes.Contains(prop.PropertyType))
                                          .Where(prop => prop.GetValue(instance, null) != null)
                                          .ToList();

            var format = string.Format("{{0,-{0}}} : {{1}}", validProperties.Max(prp => prp.Name.Length));

            return string.Join(
                     Environment.NewLine,
                     validProperties.Select(prop => string.Format(format,
                                                                  prop.Name,
                                                                  (arrayTypes.Contains(prop.PropertyType) ? string.Join(", ", (IEnumerable<string>)prop.GetValue(instance, null))
                                                                                                          : prop.GetValue(instance, null)))));
        }

       public static void Swap<T>(T lhs, T rhs) where T : class
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public static void Swap<T>( ref T lhs, ref T  rhs) where T : struct
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
