using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace TestSample
{
    [TestClass]
    public class HttpClientSample
    {
        private HttpClient httpClient;
        private Mock<HttpMessageHandler> mockMessageHandler = new Mock<HttpMessageHandler>();

        [TestInitialize]
        public void Init()
        {
            httpClient = new HttpClient(mockMessageHandler.Object);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var httpMethod = new HttpMethod("Post");
            var url = "Https://UnitTest.com";
            var content = new StringContent("StringContent");
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            mockMessageHandler.Protected().Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(
                m => "application/json" == m.Content.Headers.ContentType.ToString()), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public void AddCustomHeader()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://abc.com");
            request.Headers.Add("x-header", new[] { "x-value" });
            request.Headers.Add("x-header2", new[] { "x-value3" });
            httpClient.SendAsync(request);

            mockMessageHandler.Protected().Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(
                m => "x-value" == m.Headers.GetValues("x-header").First() && "x-value3" == m.Headers.GetValues("x-header2").First()), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public void Log_HttpResponse_RequestIsStringContent()
        {
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken t) => Task.Factory.StartNew(m => new HttpResponseMessage
                {
                    Content = new StringContent("hello")
                }, request, t));

            var response = httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("Get"), new Uri("http://test")), new CancellationToken());
            Assert.AreEqual(response.Result.Content.ReadAsStringAsync().Result, "hello");
        }

        [TestMethod]
        public void Log_HttpResponse_RequestIsObjectContent()
        {
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken t) => Task.Factory.StartNew(m => new HttpResponseMessage
                {
                    Content = new ObjectContent<object>(new
                    {
                        return1 = "A",
                        return2 = "B"
                    }, new JsonMediaTypeFormatter())
                }, request, t));

            var response = httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("Get"), new Uri("http://test")), new CancellationToken());
            Console.Write(response.Result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(response.Result.Content.ReadAsStringAsync().Result, "hello");
        }

        [TestMethod]
        public void Log_HttpResponse_GetRequestFromResponse()
        {
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken t) => Task.Factory.StartNew(m => new HttpResponseMessage
                {
                    Content = new ObjectContent<object>(new
                        {
                            return1 = "A",
                            return2 = "B"
                        }, new JsonMediaTypeFormatter()),

                    RequestMessage = request
                }, request, t));

            var httpRequestMessage = new HttpRequestMessage(new HttpMethod("Post"), new Uri("http://test"));
            httpRequestMessage.Content = new StringContent("httpRequestContent");
            var response = httpClient.SendAsync(httpRequestMessage, new CancellationToken()).Result;

            Console.Write(response.RequestMessage.Content.ReadAsStringAsync().Result);
        }
    }
}
