using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Diagnostics;

    [TestClass]
    public class EventLogSample
    {
        [TestMethod]
        public void TestMethod1()
        {
            //EventLog.WriteEntry("EventLogSample", "Hello World");
            // Assert.IsTrue(EventLog.SourceExists("EventLogSample"));
            // //EventLog.CreateEventSource("EventLogSample2","BM");
            // Assert.IsTrue(EventLog.SourceExists("EventLogSample2"));

            EventLog myLog = new EventLog();
            myLog.Source = "TestEventLogSource1";
            myLog.WriteEntry("Test");

            EventLog myTestEvenLog = new EventLog("TestEvenLog");


            EventLog.CreateEventSource("TestEventLogSource3", "TestEvenLog");

            myTestEvenLog.Source = "TestEventLogSource3";
            myTestEvenLog.WriteEntry("Test");
        }

        [TestMethod]
        public void TestMethod2()
        {
  
            EventLog myTestEvenLog = new EventLog();
            Console.Write(EventLog.LogNameFromSourceName("testps", "."));
            //EventLog.SourceExists();
            myTestEvenLog.Source = "testps";
            myTestEvenLog.WriteEntry("testps");

        }
    }
}
