using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestSample
{
    [TestClass]
    public class VirtualFunctionSample
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                throw new Exception("This is outter exeption.", new BmHttpResponseException(HttpStatusCode.Accepted));
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }

    public class BmHttpResponseException : HttpResponseException
    {
        /// <summary>
        /// Initializes a new instance of the BmHttpResponseException class.
        /// </summary>
        public BmHttpResponseException(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BmHttpResponseException class.
        /// </summary>
        /// <param name="response">The response message.</param>
        public BmHttpResponseException(HttpResponseMessage response)
            : base(response)
        {
        }

        ///<summary>
        /// Override the default ToString method, so that can log more information in HttpResponseMessage such as the status code and request body
        /// </summary>
        public override string ToString()
        {
            return "hello" + base.ToString();
        }
    }
}
