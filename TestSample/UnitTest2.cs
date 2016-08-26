using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Security.Cryptography;
    using System.Text;

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var data = ComputeHashKeyData("PCSbamtest2012@hotmail.com");
            Console.Write(data);

        }

        private string ComputeHashKeyData(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                byte[] data = Encoding.Default.GetBytes(value);
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] hashValue = mySHA256.ComputeHash(data);
                return ConvertHashValueToString(hashValue);
            }
            return value;
        }

        private string ConvertHashValueToString(byte[] hashValue)
        {
            string data = string.Empty;
            for (int i = 0; i < hashValue.Length; i++)
            {
                data += String.Format("{0:X2}", hashValue[i]);
            }
            return data;
        }
    }
}
