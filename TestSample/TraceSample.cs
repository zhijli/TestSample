using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;

    [TestClass]
    public class TraceSample
    {
        [TestMethod]
        public void TraceTest_static()
        {
            /* Create a ConsoleTraceListener and add it to the trace listeners. */
            using (Logger.LogAction("TraceTest", "some data"))
            {
            }
            
            var sw = new TraceSwitch("DisplayName", "Description");
            sw.Level = TraceLevel.Error;

            var myListener = new TextWriterTraceListener("log.txt");

            

            Trace.TraceInformation("Trace Information");
            Trace.TraceError("Trace Error");
        }

        [TestMethod]
        public void TraceTest_TraceSource()
        {
            var ts = new TraceSource("myTraceSource");

            ts.Listeners.Add(new DefaultTraceListener());
            ts.Listeners.Add(new ConsoleTraceListener());
            ts.TraceEvent(TraceEventType.Error,1,"error!");
            ts.TraceInformation("TraceInformation");
        }
    }

    public class LogListener
    {
        public virtual void LogActionStart(string actionName, params object[] args)
        {
            Trace.WriteLine(
                DateTime.UtcNow.ToString("O") +
                " Action start: '" + actionName +
                (args != null && args.Any() ? "' arguments:" +
                    args.Aggregate(string.Empty, (a, s) => a + " " + (s != null ? "'" + s.ToString() + "'" : "'NULL'")) :
                    "'"));
        }

        public virtual void LogActionEnd(string actionName, params object[] args)
        {
            Trace.WriteLine(
                DateTime.UtcNow.ToString("O") +
                " Action end: '" + actionName +
                (args != null && args.Any() ? "' arguments:" +
                    args.Aggregate(string.Empty, (a, s) => a + " " + (s != null ? "'" + s.ToString() + "'" : "NULL")) :
                    "'"));
        }
    }

    public static class Logger
    {
        static Logger()
        {
            Listener = new LogListener();
        }

        public static LogListener Listener { get; set; }

        public static void LogActionStart(string actionName, params object[] args)
        {
            LogListener listener = Listener;
            if (listener != null)
            {
                try
                {
                    listener.LogActionStart(actionName, args);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void LogActionEnd(string actionName, params object[] args)
        {
            LogListener listener = Listener;
            if (listener != null)
            {
                try
                {
                    listener.LogActionEnd(actionName, args);
                }
                catch (Exception)
                {
                }
            }
        }

        public static LogActionClass LogAction(string actionName, params object[] args)
        {
            LogListener listener = Listener;
            if (listener != null)
            {
                try
                {
                    listener.LogActionStart(actionName, args);
                }
                catch (Exception)
                {
                }
            }

            return new LogActionClass(() => LogActionEnd(actionName, listener));
        }

        private static void LogActionEnd(string actionName, LogListener listener)
        {
            if (listener != null)
            {
                listener.LogActionEnd(actionName);
            }
        }

        public class LogActionClass : IDisposable
        {
            private readonly Action onDispose;

            public LogActionClass(Action onDispose)
            {
                this.onDispose = onDispose;
            }

            public void Dispose()
            {
                this.onDispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
