using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSample.Interview
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml.Serialization;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var webRequestHandler = new WebRequestHandler();
            var cert = GetCertificate("347041dd76ed4ed57cb964bfed47d3bc1994e0b7", X509FindType.FindByThumbprint);
            webRequestHandler.ClientCertificates.Add(cert);


            var uri = new Uri(
                    "https://accounts.cp.microsoft-int.com/accounts/search-by-profile?first_name=Tom&last_name=slick");

            var httpClient = new HttpClient(webRequestHandler);


            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri(
                        "https://accounts.cp.microsoft-int.com/accounts/search-by-profile?first_name=Tom&last_name=slick"),
            };
            message.Headers.Add("api-version", "2014-09-01");
            message.Headers.Add("x-ms-correlation-id", "4CC9DC0B-8E59-439D-A358-4CB7BD4A07A8");
            message.Headers.Add("x-ms-tracking-id", "350FCEE4-2351-41C6-B4FA-C36E3DAC35E7");
            var response = httpClient.SendAsync(message).Result;

            Console.Write(response.Content.ReadAsStringAsync().Result);

            var account = response.Content.ReadAsAsync<ModernAccountInfo>();
        }

        private string GetResponse(HttpResponseMessage response)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Request: \r\n");
            stringBuilder.Append("Headers: ");
            stringBuilder.Append(response.RequestMessage.Headers);
            stringBuilder.Append(", Content: \r\n");
            stringBuilder.Append(response.RequestMessage.Content.ReadAsStringAsync().Result);
            stringBuilder.Append("\r\n ========================================================================\r\n");
            stringBuilder.Append("Response: \r\n");
            stringBuilder.Append("  StatusCode: ");
            stringBuilder.Append((int)response.StatusCode);
            stringBuilder.Append("  , ReasonPhrase: '");
            stringBuilder.Append(response.ReasonPhrase ?? "<null>");
            stringBuilder.Append("  ', Version: ");
            stringBuilder.Append((object)response.Version);
            stringBuilder.Append("  , Content: \r\n");
            stringBuilder.Append(response.Content == null ? "<null>" : response.Content.ReadAsStringAsync().Result);
            //stringBuilder.Append(", Headers:\r\n");
            //stringBuilder.Append(response.Content.Headers);


            return ((object)stringBuilder).ToString();

        }

        public X509Certificate2 GetCertificate(string value, X509FindType findType)
        {
            var store = new X509Store(StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(findType, value, validOnly: true);

                if (certificates.Count > 0)
                {
                    return certificates[0];
                }
            }
            finally
            {
                store.Close();
            }

            return null;
        }
    }

    public class ProfileInfo
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string ProfileId { get; set; }

        /// <summary>
        /// Gets or sets type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets consumer profile resource
        /// </summary>
        [JsonProperty(PropertyName = "consumer")]
        public ConsumerProfileResource Consumer { get; set; }

        /// <summary>
        /// Gets or sets Intellectual Property Services
        /// </summary>
        [JsonProperty(PropertyName = "intellectual_property_services")]
        public IntellectualPropertyServicesProfileResource IntellectualPropertyServices { get; set; }
    }

    public class AddressInfo : AddressResource
    {
        /// <summary>
        /// Gets or sets the default address
        /// </summary>
        [JsonProperty(PropertyName = "default_address")]
        public string DefaultAddress { get; set; }
    }

    public class ModernAccountInfo
    {
        public ProfileInfo ProfileInfo { get; set; }

        public AddressInfo AddressInfo { get; set; }
    }

    /// <summary>
    /// ISVProfileResource entity
    /// </summary>
    public class IntellectualPropertyServicesProfileResource : ProfileResource
    {
        /// <summary>
        /// Gets or sets first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the nationality
        /// </summary>
        [JsonProperty(PropertyName = "friendly_name")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets email address
        /// </summary>
        [JsonProperty(PropertyName = "email_address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets culture
        /// </summary>
        [JsonProperty(PropertyName = "culture")]
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the default payment instrument id
        /// </summary>
        [JsonProperty(PropertyName = "default_payment_instrument_id")]
        public string DefaultPaymentInstrumentId { get; set; }

        /// <summary>
        /// Gets or sets Tax Profile Id
        /// </summary>
        [JsonProperty(PropertyName = "default_tax_profile_id")]
        public string DefaultTaxProfileId { get; set; }

        /// <summary>
        /// Gets or sets PayoutExternalReferenceId. This is only done for legacy reason to support M2. In M3, we will remove this field. This field stores BDKId
        /// </summary>
        [JsonProperty(PropertyName = "payout_external_reference_id")]
        public string PayoutExternalReferenceId { get; set; }
    }

    public class AddressResource : HalResource
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address line 1
        /// </summary>
        [JsonProperty(PropertyName = "address_line1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2
        /// </summary>
        [JsonProperty(PropertyName = "address_line2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the address line 3
        /// </summary>
        [JsonProperty(PropertyName = "address_line3")]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or sets the postal code
        /// </summary>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }
    }

    public class ProfileResource : HalResource
    {
        /// <summary>
        /// Gets or sets account id
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Snapshot
        /// </summary>
        [JsonProperty(PropertyName = "snapshot_id")]
        public string SnapshotId { get; set; }

        /// <summary>
        /// Gets or sets type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets DefaultAddressId
        /// </summary>
        [JsonProperty(PropertyName = "default_address_id")]
        public string DefaultAddressId { get; set; }
    }

    public class HalResource
    {
        /// <summary>
        /// Gets HAL links
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        [XmlIgnore]
        public Dictionary<string, RestLink> Links { get; set; }

        /// <summary>
        /// Gets REST object type name
        /// </summary>
        [JsonProperty("object_type")]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets REST contract version
        /// </summary>
        [JsonProperty(PropertyName = "contract_version")]
        public string Version { get; set; }
    }

    public class RestLink
    {
        /// <summary>
        /// Gets or sets Hyper Reference
        /// </summary>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the Http method
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }
    }

    public class ConsumerProfileResource : ProfileResource
    {

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the nationality
        /// </summary>
        [JsonProperty(PropertyName = "nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the Birth date
        /// </summary>
        [JsonProperty(PropertyName = "birth_date")]
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets email address
        /// </summary>
        [JsonProperty(PropertyName = "email_address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets HCTI
        /// </summary>
        [JsonProperty(PropertyName = "hcti")]
        public string HCTI { get; set; }

        /// <summary>
        /// Gets or sets culture
        /// </summary>
        [JsonProperty(PropertyName = "culture")]
        public string Culture { get; set; }
    }
}
