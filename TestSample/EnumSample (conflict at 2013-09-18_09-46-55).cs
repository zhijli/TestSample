using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.Diagnostics;
    using System.Globalization;

    [TestClass]
    public class EnumSample
    {
        #region Init

        [AttributeUsage(AttributeTargets.Field)]
        public class EnumStringAttribute : Attribute
        {
            public string str;

            public EnumStringAttribute(string str)
            {
                this.str = str;
            }

            public override string ToString()
            {
                return this.str;
            }
        }

        enum Color
        {
            [EnumString("This is Black")]
            Black,
            White = 10,
            Red = 100,
            Blue = 1000
        }

        #endregion

        #region Test sample
        //Random generator Enum item

        [TestMethod]
        public void RadomGeneratorEnumItem()
        {
            Color color = Color.Black;
            while (true)
            {
                //This is WRONG, it won't work if the enum value is not 0,1,2,3,.....
                color = (Color)Color.Black.WrongRandomItem();
                Trace.TraceInformation(color.ToString());

                //This is Bad, needn't use Parse 
                color = (Color)Color.Black.BadRandomItem();
                Trace.TraceInformation(color.ToString());

                //This is OK.
                color = (Color)Color.Black.GoodRandomItem();
                Trace.TraceInformation(color.ToString());

                //Better, use Generic to prevent convertion
                color = Color.Black.BetterRandomItem();
                Trace.TraceInformation(color.ToString());

                //More reasonable to generate random Item using Color instead of Color.Black 
                //But still need convertion
                color = (Color)typeof(Color).AnotherRandomItem();
                Trace.TraceInformation(color.ToString());
                Trace.TraceInformation("=============================");
            }
        }


        //Use attribute to customer the enum to string. It's impossible to override the toString() method
        [TestMethod]
        public void DefineEnumString()
        {
            Assert.AreEqual("Black", Color.Black.ToString());
            Assert.AreEqual("This is Black", Color.Black.EnumString());
            Assert.AreEqual("White", Color.White.ToString());
            Assert.AreEqual("White", Color.White.EnumString());
        }

        [TestMethod]
        public void TestIntParse()
        {
            var strInt = "5";
            int? digit;

                digit = int.Parse(strInt, CultureInfo.InvariantCulture);

            Console.Write(digit);
            Console.Write(digit.Value);
        }
        #endregion

        #region



        #endregion
    }
}
