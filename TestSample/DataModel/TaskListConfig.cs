//-----------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Commerce.Tools.BatchService.Common
{
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related classes")]
    public class TaskListConfig
    {
        public static readonly string TaskListConfigKey = "ui.tasklist.json";

        [JsonProperty("c")]
        public TaskCategory[] Categories { get; set; }

        [JsonProperty("p")]
        public TaskPartner[] Partners { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related classes")]
    public class TaskCategory
    {
        [JsonProperty("cp")]
        public bool CheckPermission { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("pn")]
        public string PermissionName { get; set; }

        [JsonProperty("t")]
        public TaskItem[] Tasks { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related classes")]
    public class TaskItem
    {
        [JsonProperty("a")]
        public TaskApiStack[] ApiStacks { get; set; }

        [JsonProperty("dn")]
        public string DisplayName { get; set; }

        [JsonProperty("e")]
        public string EntryPoint { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("t")]
        public string Templates { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related classes")]
    public class TaskApiStack
    {
        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("t")]
        public string Template { get; set; }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related classes")]
    public class TaskPartner
    {
        [JsonProperty("dn")]
        public string DisplayName { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("ds")]
        public string Description { get; set; }
    }
}
