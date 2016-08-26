// <copyright file="CertificateManager.cs" company="Microsoft Coporation">
//     Copyright (c) Microsoft 2013. All rights reserved.
// </copyright>

namespace Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an address referenced by value in another object.
    /// </summary>
    /// <remarks>
    /// "Complex type".
    /// Addresses are referenced by various objects in separate segments of the commerce platform (commerce account, credit card, order, ...).
    /// </remarks>
    public class CommerceAddress
    {
        /// <summary>
        /// Gets or sets the given name of the addressee for this address.
        /// </summary>
        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the surname of the addressee for this address.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the first line of this address.
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of this address.
        /// </summary>
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the third line of this address.
        /// </summary>
        /// <remarks>
        /// TODO: do we remove this one?
        /// </remarks>
        public string Line3 { get; set; }

        /// <summary>
        /// Gets or sets the city portion of this address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state portion of this address.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal code portion of this address.
        /// </summary>
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country (ISO2 code) portion of this address.
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number portion of this address.
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
