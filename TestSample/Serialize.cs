using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestSample
{
    class SerializeClass
    {
        public string name;
    }

    [TestClass]
    public class Serialize
    {
        [TestMethod]
        public void TestMethod1()
        {
            SerializeClass serializeClass = new SerializeClass() { name = "hello" };

            XmlSerializer serialize = new Serialize();
            
        }
    }
}
