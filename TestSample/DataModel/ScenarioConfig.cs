//-----------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Commerce.Tools.BatchTool.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot(ElementName = "ScenarioConfig")]
    public class ScenarioConfig
    {
        [XmlArray("Categories"), XmlArrayItem("Category")]
        public List<Category> Categories;
    }

    [Serializable]
    public class Category
    {
        [XmlElement(ElementName = "CategoryName")]
        public string CategoryName;

        [XmlArray("SubCategories"), XmlArrayItem("SubCategory")]
        public List<SubCategory> SubCategories;
    }

    [Serializable]
    public class SubCategory
    {
        [XmlElement(ElementName = "SubCategoryName")]
        public string SubCategoryName;

        [XmlArray("Scenarios"), XmlArrayItem("Scenario")]
        public List<ScenarioContent> Scenarios;
    }

    [Serializable]
    public class ScenarioContent
    {
        [XmlElement(ElementName = "ScenarioName")]
        public string ScenarioName;
    }
}