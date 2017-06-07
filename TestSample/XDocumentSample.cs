using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System;
    using System.IO;
    using System.Xml;

    [TestClass]
    [DeploymentItem(@"data\ScenarioConfig.xml")]
    public class XDocumentSample
    {
        [TestMethod]
        public void DemoForHowToUseXDocument()
        {
            XDocument srcTree = new XDocument(
  new XComment("This is a comment"),
  new XElement("Root",
      new XElement("Child1", "data1"),
      new XElement("Child2", "data2"),
      new XElement("Child3", "data3"),
      new XElement("Child2", "data4"),
      new XElement("Info5", "info5"),
      new XElement("Info6", "info6"),
      new XElement("Info7", "info7"),
      new XElement("Info8", "info8")
  )
);

            XDocument doc = new XDocument(
                new XComment("This is a comment"),
                new XElement("Root",
                    from el in srcTree.Element("Root").Elements()
                    where ((string)el).StartsWith("data")
                    select el
                )
            );
            Console.WriteLine(doc);
        }
    }
}
