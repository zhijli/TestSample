using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            //var textReader = new TextReader();
            //var streamReader = new StreamReader();

            //var stringReader = new StringReader();
            //var stream = Stream.Null;
            //var nullStream = new NullStream();
            //var memoryStream = new MemoryStream();
            //var fileStream = new FileStream();

            GC.SuppressFinalize(this);
        }
    }
}
