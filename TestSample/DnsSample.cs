using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    [TestClass]
    public class DnsSample
    {
        [TestMethod]
        public void GetHostAddressByName()
        {
            string name = "BatchService.microsoft.com";

            //map in host file:
            //10.172.14.172 BatchService.microsoft.com
            const string expectIp = "10.172.14.172"; 
            var result = Dns.GetHostAddresses("BatchService.microsoft.com");
            Assert.AreEqual(expectIp, result[0].ToString());
        }

        [TestMethod]
        public void GetHostAddressByIp()
        {
            string name = "BatchService.microsoft.com";
            const string ip = "10.172.14.172";
            var result = Dns.GetHostAddresses(ip);
            Assert.AreEqual(ip,result[0].ToString());
        }

        [TestMethod]
        public void GetHostNameByIp()
        {
            string name = "BatchService.microsoft.com";
            const string ip = "10.172.14.172";
            var result = Dns.GetHostEntry(ip);
            Assert.AreEqual(name, result.HostName);
        }
    }
}
