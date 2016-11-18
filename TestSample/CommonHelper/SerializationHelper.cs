//-----------------------------------------------------------------------
// <copyright company="Microsoft">
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Microsoft.Commerce.Tools.ToolsContainer.Common.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.ServiceModel.Channels;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Xml;
    using System.Xml.Serialization;

    public static class SerializationHelper
    {
        private static readonly string nameSpace = "urn:schemas-microsoft-com:billing-data";

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T">type.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>Target object.</returns>
        public static T Deserialize<T>(string xml)
        {
            var obj = Deserialize(xml, typeof(T));
            if (obj != null)
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static object Deserialize(string xml, Type type)
        {
            try
            {
                var serializer = new XmlSerializer(type);
                using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
                {
                    return serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return null;
        }
    }
}