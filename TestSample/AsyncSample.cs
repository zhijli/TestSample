﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.Remoting.Messaging;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    [TestClass]
    public class AsyncSample
    {
        [TestMethod]
        public void TestMethod1()
        {
            var task = ProcessAsync();
            var length = task.Result;
            Console.Write(string.Format("The length is {0}", length));
        }

        [TestMethod]
        public void ExecutionContextTest()
        {
            Console.WriteLine("Main thread:{0}", Thread.CurrentThread.ManagedThreadId);
            var ec = ExecutionContext.Capture();

            ExecutionContext.Run(ec, t => Console.WriteLine("ExecutionContext.Run thread:{0}", Thread.CurrentThread.ManagedThreadId), null);
            Task.Run(() => Console.WriteLine("Task.Run thread:{0}", Thread.CurrentThread.ManagedThreadId));

            ec = ExecutionContext.Capture();
            ExecutionContext.Run(ec,
                t => Task.Run(() => Console.WriteLine("ExecutionContext.Run (Task.Run) thread:{0}", Thread.CurrentThread.ManagedThreadId)),
                null);
        }

        [TestMethod]
        public void GetProgress()
        {
            var pro = new Progress<int>(i => Console.Write("Progress:{0}.", i));
            var num = CountAsync(1000, pro);
            Console.Write("End...");
        }


        private int CountAsync(int num, IProgress<int> pro)
        {

            for (int i = 0; i <= num; i++)
            {
                pro.Report(i);
            }
            return num;
        }

        private async Task<int> Process()
        {
            var client = new HttpClient();
            var task = client.GetStringAsync("http://baidu.com");

            string str = await task;
            return str.Length;

        }

        private async Task<int> ProcessAsync()
        {

            var client = new HttpClient();
            Task<string> task = client.GetStringAsync("http://baidu.com");

            string str = await task;
            return str.Length;
        }

        [TestMethod]
        public void AsyncDemo()
        {
            // Create an instance of the test class.
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            // The threadId parameter of TestMethod is an out parameter, so 
            // its input value is never used by TestMethod. Therefore, a dummy 
            // variable can be passed to the BeginInvoke call. If the threadId 
            // parameter were a ref parameter, it would have to be a class- 
            // level field so that it could be passed to both BeginInvoke and  
            // EndInvoke. 
            int dummy = 0;

            // Initiate the asynchronous call, passing three seconds (3000 ms) 
            // for the callDuration parameter of TestMethod; a dummy variable  
            // for the out parameter (threadId); the callback delegate; and 
            // state information that can be retrieved by the callback method. 
            // In this case, the state information is a string that can be used 
            // to format a console message.
            IAsyncResult result = caller.BeginInvoke(3000,
                out dummy,
                new AsyncCallback(CallbackMethod),
                "The call executed on thread {0}, with return value \"{1}\".");

            Console.WriteLine("The main thread {0} continues to execute...",
                Thread.CurrentThread.ManagedThreadId);

            // The callback is made on a ThreadPool thread. ThreadPool threads 
            // are background threads, which do not keep the application running 
            // if the main thread ends. Comment out the next line to demonstrate 
            // this.
            Thread.Sleep(4000);

            Console.WriteLine("The main thread ends.");
        }

        // The callback method must have the same signature as the 
        // AsyncCallback delegate. 
        static void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;

            // Retrieve the format string that was passed as state  
            // information. 
            string formatString = (string)ar.AsyncState;

            // Define a variable to receive the value of the out parameter. 
            // If the parameter were ref rather than out then it would have to 
            // be a class-level field so it could also be passed to BeginInvoke. 
            int threadId = 0;

            // Call EndInvoke to retrieve the results. 
            string returnValue = caller.EndInvoke(out threadId, ar);

            // Use the format string to format the output message.
            Console.WriteLine(formatString, threadId, returnValue);
        }
    }

    public class AsyncDemo
    {
        // The method to be executed asynchronously. 
        public string TestMethod(int callDuration, out int threadId)
        {
            Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }
    // The delegate must have the same signature as the method 
    // it will call asynchronously. 
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);
}
