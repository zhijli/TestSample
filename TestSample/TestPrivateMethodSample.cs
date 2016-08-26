using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    [TestClass]
    public class TestPrivateMethodSample
    {
        [TestMethod]
        public void TestPrivateMethod()
        {
            var t = new SomeClass();
            int num = 3;
            var privateT = new PrivateObject(t);
            
            var result = (bool) privateT.Invoke("IsOdd", num);

            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void Customer_Accessor()
        //{
        //    var t = new SomeClass();
        //    int num = 3;
        //    var accessor = new Customer_Accessor(new PrivateObject(t));

        //    var result = (bool)privateT.Invoke("IsOdd", num);

        //    Assert.IsTrue(result);
        //}

        class SomeClass
        {
            private bool IsOdd(int num)
            {
                return num%2 == 1;
            }
        }
    }
}
