using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    [TestClass]
    public class RandomSample
    {
        [TestMethod]
        public void TestMethod2()
        {
            //143337951
            //150666398
            //1663795458
            //1097663221
            //1712597933
            //1776631026
            //356393799
            //1580828476
            //558810388
            //1086637143
            Random rnd = new Random(12345);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(rnd.Next(int.MaxValue));
            }

        }

        [TestMethod]
        public void TestMethod1()
        {
            Random rnd = new Random(1234);
            while (rnd.Next(int.MaxValue) != 143337951)
            {
                rnd.Next(int.MaxValue);
            }
            for (int i = 0;i < 10;i++)
            {
                Console.WriteLine(rnd.Next(int.MaxValue));
            }

        }
    }
}
