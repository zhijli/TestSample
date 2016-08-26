using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestSample
{
    [TestClass]
    public class JsonConvertSample
    {
        [TestMethod]
        public void JsonSerialize()
        {
            string serializeStr = "{\"Id\":1,\"Name\":\"Tom\"}";
            var order = new Order(1, "Tom");
            var str = JsonConvert.SerializeObject(order);
            Assert.AreEqual(serializeStr, str);
        }

        [TestMethod]
        public void JsonDeserialize()
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
        public void DebugInfoSample()
        {
            var expectOrder = new Order(1, "Tom");

            //var orderStr = JsonConvert.SerializeObject(expectOrder, new JsonSerializerSettings(
            //    ));

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Include;
            serializer.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            var sw = new StringWriter();
            serializer.Serialize(sw, expectOrder);
            Console.WriteLine(sw.ToString());
        }

        [TestMethod]
        public void DebugInfoSample_Fail()
        {
            var expectOrder = new Order(1, "Tom");
            var orderStr = new JavaScriptSerializer().Serialize(expectOrder);
            Console.WriteLine(orderStr);
        }
    }

    class Order
    {
        public Order(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.OrderRefference = this;
        }
        public int Id;
        public string Name;
        Order OrderRefference;
    }


}
