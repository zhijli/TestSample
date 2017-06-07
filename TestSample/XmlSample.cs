using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Commerce.Tools.BatchService.Domain;
using Microsoft.Commerce.Tools.BatchTool.Models.ViewModel;
using Microsoft.Commerce.Tools.ToolsContainer.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using global::Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models;

    [TestClass]
    [DeploymentItem(@"data\AccountInfo.xml")]
    [DeploymentItem(@"data\BatchScenarioSchema.xml")]
    [DeploymentItem(@"data\ScenarioConfig.xml")]
    [DeploymentItem(@"data\NewScenario.xml")]
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
              using (var xmlReader = System.Xml.XmlReader.Create(new StringReader(xmlStr)))
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

        [TestMethod]
        public void DeserializeAttribute()
        {

            var xmlSerializer = new XmlSerializer(typeof(BatchScenarioSchema));
            FileStream file = new FileStream("BatchScenarioSchema.xml", FileMode.OpenOrCreate);
            var ca = (BatchScenarioSchema)xmlSerializer.Deserialize(file);
        }

        [TestMethod]
        public void SerializeAttribute()
        {

            var xmlSerializer = new XmlSerializer(typeof(BatchScenarioSchema));
            FileStream file = new FileStream("BatchScenarioSchema.xml", FileMode.OpenOrCreate);
            var value = (BatchScenarioSchema)xmlSerializer.Deserialize(file);

            var output = string.Empty;
            using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
            {
                XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented };
                xmlSerializer.Serialize(writer, value);
                output = sw.GetStringBuilder().ToString();
            }

            Console.Write(output);

            var output1 = string.Empty;
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture))
            {
                xmlSerializer.Serialize(sw ,value);
                output1 = sw.GetStringBuilder().ToString();
            }
            Console.Write(output1);


            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit };
            using (var sr = new StringReader(output))
            using (var xtr = System.Xml.XmlReader.Create(sr, settings))
            {
                var result = xmlSerializer.Deserialize(xtr) as BatchScenarioSchema;
            }
        }

        [TestMethod]
        public void ScenarioConfig()
        {
            FileStream file = new FileStream("ScenarioConfig.xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(file);
            var sc = SerializationHelper.Deserialize<ScenarioConfig>(sr.ReadToEnd());

            FileStream file1 = new FileStream("NewScenario.xml", FileMode.OpenOrCreate);
            var sr1 = new StreamReader(file1);
            var ns = SerializationHelper.Deserialize<ScenarioConfig>(sr1.ReadToEnd());

            

        }

        [TestMethod]
        public void ScenarioConfig1()
        {
            FileStream file = new FileStream("ScenarioConfig.xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(file);
            var sc = SerializationHelper.Deserialize<ScenarioConfig>(sr.ReadToEnd());

            FileStream file1 = new FileStream("NewScenario.xml", FileMode.OpenOrCreate);
            var sr1 = new StreamReader(file1);
            var ns = SerializationHelper.Deserialize<ScenarioConfig>(sr1.ReadToEnd());



        }
    }
}
