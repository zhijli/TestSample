using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample.DesignPattern
{
    using System.Threading;

    [TestClass]
    public class SingletonSample
    {
        [TestMethod]
        public void SingletonTest()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = i;
                var thread = new Thread(() => this.Process(temp));
                thread.Start();
            }
            Console.WriteLine("End");
        }

        private void Process(int tid)
        {
            int pInt = 0;
            var instance = Singleton.GetInstance();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Tid:{0}", tid);
                instance.AddCount();
                Console.WriteLine("\t Count: {0}", instance.GetCount());
            }
        }
    }

    public class Singleton
    {
        private Singleton()
        {
        }

        private static Singleton _instance = null;
        private readonly static object _lock = new object();

        private int _count;

        //public static Singleton GetInstance()
        //{
        //    //DoubleCheckLock for mutilple thread
        //    if (_instance == null)
        //    {
        //        lock (_lock)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new Singleton();
        //            }
        //        }
        //    }

        //    return _instance;
        //}

        public static Singleton GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    Console.WriteLine("Create Singleton...");
                    _instance = new Singleton();
                    Console.WriteLine("Create Singleton done!");
                }
            }

            return _instance;
        }

        public void AddCount()
        {
            _count++;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}

//public sealed class Singleton
//{
//    private static Singleton instance = null;
//    private Singleton() { }
//    public static Singleton Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = new Singleton();
//            }
//            return instance;
//        }
//    }
//}

//public sealed class Singleton
//{
//    private static Singleton instance = null;
//    private static readonly object padlock = new object();

//    private Singleton() { }
//    public static Singleton Instance
//    {
//        get
//        {
//            lock (padlock)
//            {
//                if (instance == null)
//                {
//                    instance = new Singleton();
//                }
//                return instance;
//            }
//        }
//    }
//}

//public sealed class Singleton
//{
//    private static Singleton instance = null;
//    private static readonly object padlock = new object();

//    Singleton() { }
//    public static Singleton Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                lock (padlock)
//                {
//                    if (instance == null)
//                    {
//                        instance = new Singleton();
//                    }
//                }
//            }
//            return instance;
//        }
//    }
//}

public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();
    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Singleton() { }
    private Singleton() { }
    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }
}

