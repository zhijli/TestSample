using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using global::Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models;

    [TestClass]
    [DeploymentItem(@"AccountInfo.xml")]
    public class XmlSample
    {
        [TestMethod]
        public void ReadXmlFile()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("AccountInfo.xml");
            var root = xDoc.DocumentElement;
            var AccountName = root.GetElementsByTagName("AccountName");
            
            var GiveName = root.GetElementsByTagName("GiveName");
            var Surname = root.GetElementsByTagName("Surname");
        }

        [TestMethod]
        public void ReadFromXmlString()
        {
            var fieldName = "//*[local-name()='name']";
            var xmlStr = "<root><users xmlns=\"abc\"><name>1</name></users></root>";
              using (var xmlReader = XmlReader.Create(new StringReader(xmlStr)))
            {
                  XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(xmlReader);

                  var root = xDoc.DocumentElement;
                var field = root.SelectSingleNode(fieldName);
                Assert.AreEqual("1", field.InnerText);
            }
        }

        [TestMethod]
        public void Serialize()
        {
            var ca = new CommerceAccount();
            ca.Id = Guid.NewGuid();
            ca.DefaultAddress = new CommerceAddress();
            ca.DefaultAddress.City = "US";

            var xmlSerializer = new XmlSerializer(typeof(CommerceAccount));
            
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw,ca);
            Console.Write(sw.ToString().Replace(Environment.NewLine, ""));
        }

        [TestMethod]
        public void Deserialize()
        {
            
            var xmlSerializer = new XmlSerializer(typeof(CommerceAccount));
            FileStream file = new FileStream("AccountInfo.xml", FileMode.OpenOrCreate);
            var ca = (CommerceAccount) xmlSerializer.Deserialize(file);

        }
    }
}
