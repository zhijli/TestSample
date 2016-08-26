using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample.Interview
{
    [TestClass]
    public class String2IntTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string str = "123";
            int expect = 123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string str = "-123";
            int expect = -123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string str = "+123";
            int expect = 123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string str = "-0123";
            int expect = -123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod5()
        {
            string str = "+0123";
            int expect = 123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod6()
        {
            string str = "0123";
            int expect = 123;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod7()
        {
            string str = "+0";
            int expect = 0;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod8()
        {
            string str = "-0";
            int expect = 0;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        public void TestMethod9()
        {
            string str = "0";
            int expect = 0;

            Assert.AreEqual(expect, String2Int(str));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod10()
        {
            string str = "--123";

            String2Int(str);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod11()
        {
            string str = "-+123";

            String2Int(str);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod12()
        {
            string str = "a123";

            String2Int(str);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void TestMethod13()
        {
            string str = "12311111111111111111111111";
            String2Int(str);
        }

        /// <summary>
        /// string to int
        /// 
        /// eg: "-123" -> -123
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int String2Int(string str)
        {
            bool isValid = false;
            int result = 0, sign = 1, i = 0;

            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException();
            }

            if (str[0] == '-')
            {
                sign = -1;
                i = 1;
            }

            if (str[0] == '+')
            {
                i = 1;
            }

            for (; i < str.Length; i++)
            {
                isValid = true;

                if (str[i] < '0' || str[i] > '9')
                {
                    throw new ArgumentException();
                }

                checked
                {
                    result = result * 10 + (str[i] - '0');
                }
                
            }

            if (!isValid)
            {
                throw new ArgumentException();
            }

            return result * sign;
        }
    }
}
