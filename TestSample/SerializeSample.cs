using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Commerce.Tools.ToolsContainer.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Xml.Serialization;

    [TestClass]
    public class SerializeSample
    {
        [TestMethod]
        public void XMLSerializer()
        {
            var serializableClass = new SerializableClass();

            var serializer = new XmlSerializer(typeof(SerializableClass));

            var textWriter = new StringWriter();
            serializer.Serialize(textWriter, serializableClass);
            Console.Write(textWriter);
        }

        [TestMethod]
        public void SoapSerializer()
        {
            var serializableClass = new SerializableClass();

            //var stream = new StreamWriter();
            SoapFormatter formatter = new SoapFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, serializableClass);
            stream.Position = 0;
            Console.WriteLine(new StreamReader(stream).ReadToEndAsync().Result);
            stream.Close();
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Creates a new TestSimpleObject object.
            SerializableClass obj = new SerializableClass();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            //Opens a file and serializes the object into it in binary format.
            Stream stream = File.Open("data.xml", FileMode.Create);
            SoapFormatter formatter = new SoapFormatter();

            //BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            //Empties obj.
            obj = null;

            //Opens file "data.xml" and deserializes the object from it.
            stream = File.Open("data.xml", FileMode.Open);
            formatter = new SoapFormatter();

            //formatter = new BinaryFormatter();

            obj = (SerializableClass)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();
        }
    }

    [Serializable]
    public class SerializableClass 
    {
        public int member1;
        public string member2;
        public string member3;
        public double member4;
        // A field that is not serialized.
        [NonSerialized()]
        public string member5;

        public SerializableClass()
        {

            member1 = 11;
            member2 = "hello";
            member3 = "hello";
            member4 = 3.14159265;
            member5 = "hello world!";
        }


        public void Print()
        {

            Console.WriteLine("member1 = '{0}'", member1);
            Console.WriteLine("member2 = '{0}'", member2);
            Console.WriteLine("member3 = '{0}'", member3);
            Console.WriteLine("member4 = '{0}'", member4);
            Console.WriteLine("member5 = '{0}'", member5);
        }

    }
}
