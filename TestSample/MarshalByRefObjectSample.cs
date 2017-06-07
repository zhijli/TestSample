// -----------------------------------------------------------------------
//  <copyright company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace TestSample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Http;
    using System.Runtime.Remoting.Channels.Tcp;
    using System.Text;
    using System.Threading.Tasks;
    using global::Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class MarshalByRefObjectSample
    {
        [TestMethod]
        public void MarshalByRefObjectSampleTest()
        {
            TcpChannel chan1 = new TcpChannel(18085);
            HttpChannel chan2 = new HttpChannel(18086);

            ChannelServices.RegisterChannel(chan1, false);
            ChannelServices.RegisterChannel(chan2, false);


            RemotingConfiguration.RegisterWellKnownServiceType(typeof(HelloServer), "SayHello", WellKnownObjectMode.Singleton);   //创建类的实例

            //使用HTTP通道得到远程对象
            HttpChannel chan3 = new HttpChannel();
            ChannelServices.RegisterChannel(chan3);
            HelloServer obj1 = (HelloServer)Activator.GetObject(
                typeof(HelloServer),
                "http://localhost:8086/SayHello");//创建类的实例
            if (obj1 == null)
            {
                System.Console.WriteLine(
                    "Could not locate HTTP server");
            }
            Console.WriteLine(
                "Client1 TCP HelloUserMethod {0}",
                obj1.HelloUserMethod(new User("张生", true))); //将类作为参数
        }

        [TestMethod]
        public void Test()
        {
            // Create an ordinary instance in the current AppDomain
            Worker localWorker = new Worker();
            localWorker.PrintDomain();

            // Create a new application domain, create an instance
            // of Worker in the application domain, and execute code
            // there.
            AppDomain ad = AppDomain.CreateDomain("New domain");
            Worker remoteWorker = (Worker)ad.CreateInstanceAndUnwrap(
                typeof(Worker).Assembly.FullName,
                "Worker");
            remoteWorker.PrintDomain();
        }

        public class HelloServer : MarshalByRefObject
        {
            public HelloServer()
            {
                Console.WriteLine("HelloServer activated");
            }


            public String HelloUserMethod(User user)
            {
                string title;
                if (user.Male)
                    title = "先生";
                else
                    title = "女士";

                Console.WriteLine("Server Hello.HelloMethod : 你好，{0}{1}", user.Name, title);

                return "你好，" + user.Name + title;
            }

        }

        [Serializable]
        public class User
        {
            public User(string name, bool male)
            {
                this.name = name;
                this.male = male;
            }

            private string name = "";
            private bool male = true;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public bool Male
            {
                get { return male; }
                set { male = value; }
            }

        }

        public class Worker : MarshalByRefObject
        {
            public void PrintDomain()
            {
                Console.WriteLine("Object is executing in AppDomain \"{0}\"",
                    AppDomain.CurrentDomain.FriendlyName);
            }
        }
    }
}