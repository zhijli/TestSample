using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    [TestClass]
    public class ThrowExceptionSample
    {
        [TestMethod]
        public void ExceptionSample()
        {
            try
            {
                try
                {
                    ThrowException();
                }
                catch (Exception e)
                {
                    Console.WriteLine("2:{0}", e);
                    var ie = new NullReferenceException("MidException", e);
                    Console.WriteLine("3:{0}", ie);
                    throw ie;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("4:{0}", e);
                var ie = new NullReferenceException("OuterException", e);
                Console.WriteLine("5:{0}", ie);
                Console.WriteLine("6:{0}", ie.Message);
                throw ie;
            }
        }

        [TestMethod]
        public void Throw()
        {
            try
            {
                ThrowException();
            }
            catch (Exception e)
            {
                //Use throw will missing one stack trace
                throw;
            }

        }

        [TestMethod]
        public void ReThrow()
        {
            try
            {
                this.ThrowException();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [TestMethod]
        public void ReThrowWithModify()
        {
            try
            {
                this.ThrowException();
            }
            catch (Exception e)
            {
                Console.Write("Modify");
                throw;
            }

        }

        [TestMethod]
        public void ReThrowEx()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                //Use throw ex will overwrite the stack trace.
                throw ex;
            }

        }

        [TestMethod]
        public void ThrowWithData()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                //Use throw ex will overwrite the stack trace.
                ex.Data.Add(sf.GetMethod().ToString(),sf.GetMethod());
                throw;
            }

        }
        private void ThrowException()
        {
            var e = new NotImplementedException("InnerException");
            Console.WriteLine("1:{0}", e);
            throw e;
        }
    }
}
