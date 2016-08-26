// <copyright file="CertificateManager.cs" company="Microsoft Coporation">
//     Copyright (c) Microsoft 2013. All rights reserved.
// </copyright>

namespace Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models
{
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a link to a resource and the method used to access it.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets the URI to the linked resource (or action).
        /// </summary>
        [JsonProperty("href")]
        public string HRef { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method to be used when using the URI indicated by HRef.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
