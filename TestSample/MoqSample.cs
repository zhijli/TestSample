using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace TestSample
{
    [TestClass]
    public class MoqSample
    {
        [TestMethod]
        public void MoqFuncTest()
        {
            var modOrder = new Mock<IOrder>();

            Assert.IsFalse(modOrder.Object.CheckName("Not Setup"));
            Assert.IsNull(modOrder.Object.Echo("Not Setup"));

            modOrder.Setup(m => m.CheckName("Right Name")).Returns(true);
            Assert.AreEqual(true, modOrder.Object.CheckName("Right Name"));
            Assert.IsFalse(modOrder.Object.CheckName("Other input"));

            modOrder.Setup(m => m.Echo(It.IsAny<string>())).Returns((string str) => str);
            Assert.AreEqual("Str1", modOrder.Object.Echo("Str1"));
            Assert.AreEqual("Str2", modOrder.Object.Echo("Str2"));

            modOrder.Setup(m => m.IsOdd(It.IsAny<int>())).Returns((int i) => i % 2 == 1);
            Assert.IsTrue(modOrder.Object.IsOdd(3));
            Assert.IsFalse(modOrder.Object.IsOdd(4));

            modOrder.Setup(m => m.ToLower(It.IsAny<string>())).Returns<string>(r => r.ToLower());
            Assert.AreEqual("abcd", modOrder.Object.ToLower("AbCd"));
            Assert.AreNotEqual("AbCd", modOrder.Object.ToLower("AbCd"));

            int count = 0;
            modOrder.Setup(m => m.GetCallCount()).Returns(() => count).Callback(() => count++);
            Assert.AreEqual(1, modOrder.Object.GetCallCount());
            Assert.AreEqual(2, modOrder.Object.GetCallCount());

        }

        [TestMethod]
        public void ModProperty()
        {
            var modOrder = new Mock<IOrder>();

            Assert.AreEqual(0, modOrder.Object.Id);
            Assert.IsNull(modOrder.Object.Name);

            modOrder.Setup(m => m.Name).Returns("Name");
            Assert.AreEqual("Name", modOrder.Object.Name);
            modOrder.Name = "Update Name";
            Assert.AreEqual("Name", modOrder.Object.Name);

            modOrder.SetupProperty(m => m.Id, 5);
            Assert.AreEqual(5, modOrder.Object.Id);
            modOrder.Object.Id = 4;
            Assert.AreEqual(4, modOrder.Object.Id);

            //var claimsCount = modOrder.IfTypeIs<ClaimsPrincipal>()
            //          .Then(a => a.Claims.Count)
            //          .Else(() => 0);
        }

        [TestMethod]
        public void ModVerify_NoSetupAndCallOnceWithCorrectParam_VerifyPass()
        {
            var modOrder = new Mock<IOrder>();
            modOrder.Object.CheckName("Right Name");
            modOrder.Verify(m => m.CheckName("Right Name"));
        }

        [TestMethod]
        public void ModVerify_NoSetupAndCallOnce_VerifyPass()
        {
            var modOrder = new Mock<IOrder>();
            modOrder.Object.CheckName("Right Name");
            modOrder.Verify(m => m.CheckName("Right Name"));
        }

        [TestMethod]
        public void ModVerify_SetupAndCallOnceWithCorrectParam_VerifyPerticulaPass()
        {
            var modOrder = new Mock<IOrder>();
            modOrder.Setup(mock => mock.CheckName("This Name")).Returns(true);
            modOrder.Object.CheckName("Right Name");
            modOrder.Verify(m => m.CheckName("Right Name"));
        }

        [TestMethod]
        public void ModVerify_SetupAndCallOnceWithCorrectParam_VerifyPass()
        {
            var modOrder = new Mock<IOrder>();
            modOrder.Setup(mock => mock.CheckName("This Name")).Returns(true);
            modOrder.Object.CheckName("Right Name");
            modOrder.Verify();
        }

        [TestMethod]
        public void ModVerify_SetupAndCallOnceWithCorrectParam_VerifyAllFailed()
        {
            var modOrder = new Mock<IOrder>();
            modOrder.Setup(mock => mock.CheckName("This Name")).Returns(true);
            modOrder.Object.CheckName("Right Name");
            modOrder.VerifyAll();
        }

        [TestMethod]
        public void ModVerify_ClassValueEqual_Failed()
        {
            var modOrder = new Mock<IOrder>();
            var guid = Guid.NewGuid();

            var request1 = new PingRequest()
            {
                Id = guid,
                Name = "Test111",
            };

            var request2 = new PingRequest()
            {
                Id = guid,
                Name = "Test111",
            };
           
            modOrder.Object.Ping(request1, guid);
            modOrder.Verify(mock => mock.Ping(request2, guid));
        }

        [TestMethod]
        public void ModVerify_ClassReferenceEqual_Pass()
        {
            var modOrder = new Mock<IOrder>();
            var guid = Guid.NewGuid();

            var request1 = new PingRequest()
            {
                Id = guid,
                Name = "Test111",
            };

            modOrder.Object.Ping(request1, guid);
            modOrder.Verify(mock => mock.Ping(request1, guid));
        }

        [TestMethod]
        public void ModVerify_DictionaryValueEqual_Pass()
        {
            var modOrder = new Mock<IOrder>();
            var guid = Guid.NewGuid();

            Dictionary<string,string> dic1 = new Dictionary<string, string>{{"key1","value1"}, {"key2","value2"}};

            Dictionary<string, string> dic2 = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            modOrder.Object.GetDic(dic1);
            modOrder.Verify(mock => mock.GetDic(dic2));
        }

        [TestMethod]
        public void ModProtectedMethod()
        {
            var modOrder = new Mock<MyOrder>();

            modOrder.Protected().Setup("Print");
            modOrder.Object.Echo("a");
            modOrder.Protected().Verify("Print", Times.Once());

        }

        public interface IOrder
        {
            int Id { get; set; }
            string Name { get; set; }
            double Price { get; set; }

            bool CheckName(string str);
            string Echo(string str);
            string ToLower(string str);
            bool IsOdd(int num);
            int GetCallCount();
            void Ping(PingRequest request, Guid guid);
            void GetDic(Dictionary<string, string> dic);
        }

        public abstract class MyOrder : IOrder
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public bool CheckName(string str)
            {
                throw new NotImplementedException();
            }

            public string Echo(string str)
            {
                Print();
                return string.Empty;
            }

            public string ToLower(string str)
            {
                throw new NotImplementedException();
            }

            public bool IsOdd(int num)
            {
                throw new NotImplementedException();
            }

            public int GetCallCount()
            {
                throw new NotImplementedException();
            }

            public void Ping(PingRequest request, Guid guid)
            {
                throw new NotImplementedException();
            }

            public void GetDic(Dictionary<string, string> dic)
            {
                throw new NotImplementedException();
            }

            protected abstract void Print();

        }

        public class PingRequest
        {
            public string Name { get; set; }

            public Guid Id { get; set; }
        }

        private class OrderManager
        {
            private List<IOrder> ordersList = new List<IOrder>();

            public List<IOrder> OrderList
            {
                get { return ordersList; }

                set { ordersList = value; }
            }

            public double TotalPrice()
            {
                double total = 0;

                if (OrderList != null)
                {
                    foreach (IOrder order in OrderList)
                    {
                        total += order.Price;
                    }
                }

                return total;
            }
        }
    }
}