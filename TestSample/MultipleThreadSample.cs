using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace TestSample
{
    using System.Data;
    using System.Threading.Tasks;

    [TestClass]
    public class MultipleThreadSample
    {
        #region Field
        [ThreadStatic]
        private static int sInt = 0;

        [ThreadStatic]
        private int gInt = 0;

        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = i;
                var thread = new Thread(() => this.Process(temp));
                thread.Start();
            }
        }

        [TestMethod]
        [TestCategory("P0")]
        //Premium  choice for .Net 4 and above
        public void MultipleThread_UseNewTask()
        {
            var myData = new IntegratedParam
          {
              Id = 1,
              Name = "Tom",
              returnValue = "N/A"
          };

            var task = new Task<string>(state =>
            {
                var data = state as IntegratedParam;
                OutputWithTid("Output from Worker. Id:{0}, Name:{1}", data.Id, data.Name);
                Thread.Sleep(1000);
                var returnValue = "This is return value!";
                return returnValue;

            }, myData);

            task.Start();
            OutputWithTid("Output from Main, Thread has been finished. ReturnValue: {0}", task.Result);
        }

        [TestMethod]
        [TestCategory("P0")]
        //Premium  choice for .Net 4 and above
        public void MultipleThread_UseNewTask_Cancel()
        {
            var cts = new CancellationTokenSource();

            var myData = new IntegratedParam
            {
                Id = 1,
                Name = "Tom",
                returnValue = "N/A"
            };

            var task = new Task<string>(state =>
                {
                    var data = state as IntegratedParam;
                    OutputWithTid("Output from Worker. Id:{0}, Name:{1}", data.Id, data.Name);
                    
                    Thread.Sleep(1000);
                    var returnValue = "This is return value!";
                    cts.Token.ThrowIfCancellationRequested();
                    return returnValue;

                }, myData, cts.Token);

            task.Start();
            cts.Cancel();
            try
            {
                OutputWithTid("Output from Main, Thread has been finished. ReturnValue: {0}", task.Result);
            }
            catch (AggregateException e)
            {
                
                   e.Handle( ex => ex is OperationCanceledException);
                   OutputWithTid("Task is cancled.");
            }
           
        }

        [TestMethod]
        [TestCategory("P1")]
        public void MultipleThread_UseNewThread()
        {
            var thread = new Thread(() =>
            {
                OutputWithTid("Output from Worker");
                Thread.Sleep(1000);
            });

            thread.Start();
            OutputWithTid("Output from Main");
            thread.Join();
        }

        [TestMethod]
        [TestCategory("P1")]
        public void MultipleThread_UseNewThreadWithParams()
        {
            var myData = new IntegratedParam
            {
                Id = 1,
                Name = "Tom",
                returnValue = "N/A"
            };

            var thread = new Thread(state =>
            {
                var data = state as IntegratedParam;
                OutputWithTid("Output from Worker. Id:{0}, Name:{1}", data.Id, data.Name);
                data.returnValue = "This is return value!";
                Thread.Sleep(1000);
            });

            thread.Start(myData);
            OutputWithTid("Output from Main, Thread may not finish. ReturnValue: {0}", myData.returnValue);
            thread.Join();
            OutputWithTid("Output from Main. Thread has been finished. ReturnValue: {0}", myData.returnValue);
        }

        [TestMethod]
        [TestCategory("P1")]
        public void MultipleThread_UseThreadPool()
        {
            var thread = ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Returning from Worker");
            });

            Console.WriteLine("Returning from Main");
        }

        [TestMethod]
        [TestCategory("P1")]
        public void MultipleThread_UseThreadPoolWithParam()
        {
            var workerDoneEvent = new ManualResetEvent(false);

            var myData = new IntegratedParam
            {
                Id = 1,
                Name = "Tom",
                returnValue = "N/A"
            };

            bool result = ThreadPool.QueueUserWorkItem(state =>
            {
                var data = state as IntegratedParam;
                OutputWithTid("Output from Worker. Id:{0}, Name:{1}", data.Id, data.Name);
                data.returnValue = "This is return value!";
                Thread.Sleep(1000);
                workerDoneEvent.Set();
            }, myData);

            OutputWithTid("Output from Main, Thread may not finish. ReturnValue: {0}", myData.returnValue);
            workerDoneEvent.WaitOne();
            OutputWithTid("Output from Main. Thread has been finished. ReturnValue: {0}", myData.returnValue);
        }

        [TestMethod]
        [TestCategory("P2")]
        //For .Net 4 or above, use Task intead.
        public void MultipleThread_UseNewThread_HandleExceptionInThread()
        {
            var thread = new Thread(
                () =>
                    SafeExecute(
                    () =>
                    {
                        OutputWithTid("Output from Worker");
                        throw new Exception("Excption in worker");
                    },
                    LogException));

            thread.Start();
            OutputWithTid("Output from Main");
            thread.Join();
        }

        [TestMethod]
        [TestCategory("P2")]
        public void FrontendThread()
        {
            var t = new Thread(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("Returning from Worker");
            });

            t.Start();

            Console.WriteLine("Returning from Main");
        }

        private void Process(int tid)
        {
            int pInt = 0;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Tid:{0}", tid);
                Console.WriteLine("\t sInt: {0}, gInt: {1} , pInt: {2}", sInt, gInt, pInt);
                Thread.Sleep(1000);
                sInt++;
                gInt++;
                pInt++;
                Console.WriteLine("\t Update value...");
                Console.WriteLine("\t sInt: {0}, gInt: {1} , pInt: {2}", sInt, gInt, pInt);
            }
        }

        private void OutputWithTid(string str, params object[] o)
        {
            Console.WriteLine("Tid: {0}. {1}", Thread.CurrentThread.ManagedThreadId, string.Format(str, o));
        }

        private void SafeExecute(Action func, Action<Exception> handler)
        {
            try
            {
                func();
            }
            catch (Exception e)
            {
                handler(e);
            }
        }

        private void LogException(Exception e)
        {
            OutputWithTid("Exception: {0}", e);
        }

        private class IntegratedParam
        {
            public int Id;
            public string Name;
            public string returnValue;
        }
    }
}
