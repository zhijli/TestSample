using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestSample
{
    [TestClass]
    public class JsonConvertSample
    {
        [TestMethod]
        public void JsonConvert_SerializeObject()
        {
            string serializeStr = "{\"Id\":1,\"Name\":\"Tom\"}";
            var order = new Order(1, "Tom");
            var str = JsonConvert.SerializeObject(order);
            Assert.AreEqual(serializeStr, str);
        }

        [TestMethod]
        public void JsonConvert_DeserializeObject()
        {
            string serializeStr = "{\"Id\":1,\"Name\":\"Tom\"}";
            var expectOrder = new Order(1, "Tom");
            var order = JsonConvert.DeserializeObject<Order>(serializeStr);
            Assert.AreEqual(expectOrder.Id, order.Id);
            Assert.AreEqual(expectOrder.Name, order.Name);
        }

        [TestMethod]
        public void test()
        {
            string serializeStr = "{\\\"Id\\\":1,\\\"Name\\\":\\\"Tom\\\"}";
            Console.Write(serializeStr);
            Console.Write(serializeStr.Replace("\\\"", "\""));
        }

        [TestMethod]
        public void JsonSerialize()
        {
            string serializeStr = "{\"Id\":1,\"Name\":\"Tom\"}";
            var expectOrder = new Order(1, "Tom");
            var serializer = new JsonSerializer();
            using (var sw = new StringWriter())
            using (var jw = new JsonTextWriter(sw))
            {
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Formatting = Formatting.Indented;
                serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Full;
                serializer.Serialize(jw,expectOrder);
                Console.WriteLine(sw.ToString());
            }
        }
    }

    class Order
    {
        public Order(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id;
        public string Name;
    }


}
