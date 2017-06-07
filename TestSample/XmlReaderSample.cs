using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSample
{
    using System;
    using System.IO;
    using System.Xml;

    [TestClass]
    [DeploymentItem(@"data\ScenarioConfig.xml")]
    public class XmlReaderSample
    {
        //https://msdn.microsoft.com/en-us/library/a3bszkbd.aspx
        [TestMethod]
        public void DemoForHowToUseXmlReader()
        {
            var fs = new FileStream("ScenarioConfig.xml", FileMode.Open);
            var setting = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Prohibit,
                ValidationType = ValidationType.None,
                Schemas = new XmlSchemaSet { },
                XmlResolver = new XmlUrlResolver(),
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                

            };
            setting.ValidationEventHandler += (sender, args) => { };

            var reader = XmlReader.Create(fs, setting);
            //reader.MoveToContent();
            // Parse the file and display each of the nodes.
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        Console.Write("<{0}", reader.Name);
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                Console.Write(" {0}=\"{1}\"", reader.Name, reader.Value);
                            }
                            // Move the reader back to the element node.
                            reader.MoveToElement();

                            //for (int i = 0; i < reader.AttributeCount; i++)
                            //{
                            //    Console.Write(" {0}", reader[i]);
                            //}
                        }
                        Console.Write(">");
                        break;
                    case XmlNodeType.Text:
                        Console.Write(reader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        Console.Write("<![CDATA[{0}]]>", reader.Value);
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        Console.Write("<?{0} {1}?>", reader.Name, reader.Value);
                        break;
                    case XmlNodeType.Comment:
                        Console.Write("<!--{0}-->", reader.Value);
                        break;
                    case XmlNodeType.XmlDeclaration:
                        Console.Write("<?xml version='1.0'?>");
                        break;
                    case XmlNodeType.Document:
                        break;
                    case XmlNodeType.DocumentType:
                        Console.Write("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
                        break;
                    case XmlNodeType.EntityReference:
                        Console.Write(reader.Name);
                        break;
                    case XmlNodeType.EndElement:
                        Console.Write("</{0}>", reader.Name);
                        break;
                    case XmlNodeType.Whitespace: //Comment this case if want to skip whitespace
                        Console.Write(reader.Value);
                        break;
                }
            }
        }
    }
}
