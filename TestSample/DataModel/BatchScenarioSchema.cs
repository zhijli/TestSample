// -----------------------------------------------------------------------
//  <copyright company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace Microsoft.Commerce.Tools.BatchService.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [XmlRoot]
    public class BatchScenarioSchema
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArray]
        [XmlArrayItem("Parameter", typeof(Parameter))]
        public List<Parameter> ScopeParaMeters { get; set; }

        [XmlArray]
        [XmlArrayItem("Parameter", typeof(Parameter))]
        public List<Parameter> InputSingleColumns { get; set; }

        [XmlArray]
        [XmlArrayItem("Parameter", typeof(Parameter))]
        public List<Parameter> InputMultipleColumns { get; set; }

        [XmlArray]
        [XmlArrayItem("Parameter", typeof(Parameter))]
        public List<Parameter> OutputColumns { get; set; }

        [XmlElement]
        public string CosmosScript { get; set; }
    }

    [DataContract(Namespace = "")]
    public class Parameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public ColumnType Type { get; set; }

        [XmlAttribute("ordinate")]
        public int Ordinate { get; set; }
    }

    [DataContract(Namespace = "")]
    public enum ColumnType
    {
        [EnumMember]
        String,

        [EnumMember]
        Boolean,

        [EnumMember]
        Int32,

        [EnumMember]
        Int64,

        [EnumMember]
        Guid,

        [EnumMember]
        DateTime,

        [EnumMember]
        Xml,

        [EnumMember]
        Json,

        [EnumMember]
        Decimal
    }
}