using System.Xml.Schema;
using Microsoft.Commerce.Tools.BatchTool.Models.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System;
    using System.IO;
    using System.Xml;

    [TestClass]
    [DeploymentItem(@"data\ScenarioConfig.xml")]
    public class XmlDocumentSample
    {
        [TestMethod]
        public void DemoForHowToUseXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5' misc='sale item'>" +
                          "<title>The Handmaid's Tale</title>" +
                          "<price>14.95</price>" +
                          "</book>");

            // Move to an element.
            XmlElement myElement = doc.DocumentElement;

            // Create an attribute collection from the element.
            XmlAttributeCollection attrColl = myElement.Attributes;

            // Show the collection by iterating over it.
            Console.WriteLine("Display all the attributes in the collection...");
            for (int i = 0; i < attrColl.Count; i++)
            {
                Console.Write("{0} = ", attrColl[i].Name);
                Console.Write("{0}", attrColl[i].Value);
                Console.WriteLine();
            }

            // Retrieve a single attribute from the collection; specifically, the
            // attribute with the name "misc".
            XmlAttribute attr = attrColl["misc"];

            // Retrieve the value from that attribute.
            String miscValue = attr.InnerXml;

            Console.WriteLine("Display the attribute information.");
            Console.WriteLine(miscValue);

        }

        [TestMethod]
        public void DemoForHowToUseXmlDocument1()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement name = doc.CreateElement("Name");
            name.InnerText = "Patrick Hines";
            XmlElement phone1 = doc.CreateElement("Phone");
            phone1.SetAttribute("Type", "Home");
            phone1.InnerText = "206-555-0144";
            XmlElement phone2 = doc.CreateElement("Phone");
            phone2.SetAttribute("Type", "Work");
            phone2.InnerText = "425-555-0145";
            XmlElement street1 = doc.CreateElement("Street1");
            street1.InnerText = "123 Main St";
            XmlElement city = doc.CreateElement("City");
            city.InnerText = "Mercer Island";
            XmlElement state = doc.CreateElement("State");
            state.InnerText = "WA";
            XmlElement postal = doc.CreateElement("Postal");
            postal.InnerText = "68042";
            XmlElement address = doc.CreateElement("Address");
            address.AppendChild(street1);
            address.AppendChild(city);
            address.AppendChild(state);
            address.AppendChild(postal);
            XmlElement contact = doc.CreateElement("Contact");
            contact.AppendChild(name);
            contact.AppendChild(phone1);
            contact.AppendChild(phone2);
            contact.AppendChild(address);
            XmlElement contacts = doc.CreateElement("Contacts");
            contacts.AppendChild(contact);
            doc.AppendChild(contacts);

            Console.WriteLine(address.OuterXml);
            Console.WriteLine(address.InnerXml);
        }
    }
}
