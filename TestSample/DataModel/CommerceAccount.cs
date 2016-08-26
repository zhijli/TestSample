// <copyright file="CertificateManager.cs" company="Microsoft Coporation">
//     Copyright (c) Microsoft 2013. All rights reserved.
// </copyright>

namespace Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a commerce account.
    /// It is primarily a way to abstract consumer and organizational identities.
    /// </summary>
    public class CommerceAccount
    {
        /// <summary>
        /// Gets or sets the ID of this commerce account.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identity of the owner of this commerce account.
        /// </summary>

        public Identity Identity { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of this commerce account.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of this commerce account.
        /// </summary>
        [JsonProperty("default_address")]
        public CommerceAddress DefaultAddress { get; set; }

        /// <summary>
        /// Gets or sets the email address that should be used when the commerce platform needs to notify the owner of this
        /// commerce account.
        /// </summary>
        [JsonProperty("notification_email_address")]
        public string NotificationEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the language used when communicating with this commerce account.
        /// </summary>
        [JsonProperty("communication_language")]
        public string CommunicationLanguage { get; set; }

        /// <summary>
        /// Gets or sets the culture used when communicating with this commerce account.
        /// </summary>
        [JsonProperty("communication_culture")]
        public string CommunicationCulture { get; set; }

        /// <summary>
        /// Gets or sets the currency code used for this commerce account.
        /// </summary>
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this commerce account is read only (or not) to the current caller.
        /// </summary>
        [JsonProperty("read_only")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// The ETag of this commerce account.
        /// </summary>
        public string ETag { get; set; }

        // Interface "implementation"
        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("contract_version")]
        public string ContractVersion { get; set; }

        [XmlIgnore]
        public Dictionary<string, Link> Links { get; set; }
    }

    public class Identifier
    {
        /// <summary>
        /// Gets or sets the type of this Identifier
        /// </summary>
        [JsonProperty("id_type")]
        public string IdType { get; set; }

        public string Id { get; set; }
    }

    /// <summary>
    /// Represents an identity
    /// </summary>
    /// <remarks>Complex type</remarks>
    public class Identity
    {
        /// <summary>
        /// The identity provider of this identity
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// The primary identifier of this identity, relative to the provider
        /// </summary>
        public Identifier Identifier { get; set; }

        /// <summary>
        /// The secondary identifier of this identity, relative to the provider
        /// and the primary identifier.
        /// </summary>
        [JsonProperty("sub_identifier")]
        public Identifier SubIdentifier { get; set; }
    }
}
