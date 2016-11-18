using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.XmlConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;

namespace TestSample
{
    using System.IO;

    [TestClass]
    public class SteamSample
    {
        [TestMethod]
        public void TestMethod1()
        {
            int nInt = 5;
            Console.Write(nInt);


            var streamReader = new StreamReader("");

            var stringReader = new StringReader("");
            var stream = Stream.Null;
            var xmlReader = System.Xml.XmlReader.Create(new StringReader("Abc"));
            
            var memoryStream = new MemoryStream();
            var fileStream = new FileStream(new SafeFileHandle(new IntPtr(), true ), FileAccess.Read);

            GC.SuppressFinalize(this);
        }
    }
}
