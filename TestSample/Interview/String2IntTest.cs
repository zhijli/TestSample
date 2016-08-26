using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample.Interview
{
    using System.Security.AccessControl;
    using System.Text;

    [TestClass]
    public class ReverseWordTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string input = "I am a student.";
            string expect = "student. a am I";

            Assert.AreEqual(expect, ReverseWord(input));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string input = "I";
            string expect = "I";

            Assert.AreEqual(expect, ReverseWord(input));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string input = "Student.";
            string expect = "Student.";

            Assert.AreEqual(expect, ReverseWord(input));
        }

        [TestMethod]
        public void TestMethod4()
        {
            string input = "";
            string expect = "";

            Assert.AreEqual(expect, ReverseWord(input));
        }
        [TestMethod]
        public void TestMethod5()
        {
            string input = " I am a student.";
            string expect = "student. a am I ";

            Assert.AreEqual(expect, ReverseWord(input));
        }
        [TestMethod]
        public void TestMethod6()
        {
            string input = "I   am a student.";
            string expect = "student. a am   I";

            Assert.AreEqual(expect, ReverseWord(input));
        }
        /// <summary>
        /// Reverse the word:
        /// 
        /// "I am a student." -> "student. a am I"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ReverseWord(string str)
        {
            if (str == null)
                return null;

            int sourceStart = 0, sourceEnd, desCurrent = str.Length - 1 , desEnd, desStart;

            var result = new char[str.Length];

            while (sourceStart < str.Length)
              {
                sourceEnd =  sourceStart;

                if (str[sourceStart] == ' ')
                {
                    while (sourceEnd < str.Length && str[sourceEnd] == ' ')
                    {
                        sourceEnd++;
                    }

                    for (int i = 0; i < sourceEnd - sourceStart; i++)
                    {
                        result[desCurrent + 1 - sourceEnd + sourceStart + i] = str[sourceStart + i];
                    }
                }
                else
                {
                    while (sourceEnd < str.Length && str[sourceEnd] != ' ')
                    {
                        sourceEnd++;
                    }

                    for (int i = 0; i < sourceEnd - sourceStart; i++)
                    {
                        result[desCurrent + 1 - sourceEnd + sourceStart + i] = str[sourceStart + i];
                    }
                }

                desCurrent = desCurrent - sourceEnd + sourceStart;
                sourceStart = sourceEnd;
            }

            return new string(result);
        }
    }
}
