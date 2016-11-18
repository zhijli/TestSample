using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Online.Administration;
using Microsoft.Online.Commerce.Bec;
using Microsoft.Platform.BatchProcessing.Plugin.AzureOnOMS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = Microsoft.Online.Administration.Version;

namespace TestSample
{
    using TestSample.Microsoft.Subscriptions.Spk.Utility;

    [TestClass]
    public class TempTest
    {

        [TestMethod]
        public void MyTestMethod()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>
            {
                {"a", "1"},
                {"b", "2"}
            };

            Foo(dic);

            Console.Write(dic["a"]);
            Console.Write(dic["c"]);

        }

        private void Foo(IDictionary<string, string> dic)
        {
            dic["a"] = "3";
            dic["c"] = "4";
        }

        [TestMethod]
        public void TestEmptyString()
        {

            var taxAddress = ParseFromXml<CommerceAddress>("");

            Assert.IsNull(taxAddress);
        }
        private static T ParseFromXml<T>(string xmlString)
        {
            using (var xmlReader = System.Xml.XmlReader.Create(new StringReader(xmlString)))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var commerceAccountToUpdate = (T)xmlSerializer.Deserialize(xmlReader);
                return commerceAccountToUpdate;
            }
        }

        [TestMethod]
        public void UrlEncoded()
        {
            var BatchId = 0;
            var UniqueId = 554688363;
            long left = BatchId * 4294967296;
            long right = UniqueId & 4294967295;
            long result = left + right;
            var result2 = (BatchId*4294967296 + (UniqueId & 4294967295)).ToString();
            var result3 = result2.Length < 10 ? result2 : result2.Substring(0, 10);
            Console.WriteLine(result3);
        }

        [TestMethod]
        public void BdkEncoded()
        {
            string encodedId = "o-uP6gAAAAAAAAEA";

            var bdkId = new BdkId(encodedId);

            Console.WriteLine("AccountId:{0} SubRefId{1}", bdkId.AccountId, bdkId.SubRefId);
        }

    

        [TestMethod]
        public void Sample()
        {
            var str = @"b.BalanceDateTime,
           b.BalanceId,

           b.BalanceProperty,
           b.BalanceStatusTypeDescription,
           b.BalanceStatusTypeId,
           b.BalanceStatusTypeKey,
           b.BillableAmount,
           b.CollectionBatchKey,
           b.CountryCode,
           b.CountryKey,
           b.CurrencyCode,
           b.CurrencyISONumber,
           b.CurrencyKey,
           b.DatabasePartitionId,
           b.DeclineCount,
           b.DeltaDateTime,
           b.DueDate,
           b.DunningCycleScheduleId,
           b.GeneralSubPaymentMethodType,
           b.InsertedBy,
           b.InsertedDateTime,
           b.IntegrationBatchKey,
           b.MobileOperatorId,
           b.OneTimePMDataNSSubPaymentMethodType,
           b.PaymentMethodId,
           b.PaymentMethodProperty,
           b.PaymentMethodTypeDescription,
           b.PaymentMethodTypeId,
           b.PaymentMethodTypeKey,
           b.ReceivedDate,
           b.RowProcessTime,
           b.SolutionName,
           b.TaskExecutionId,
           b.TaxAmount,
           b.TaxIncluded,
           b.TemptCount,
           b.UpdatedBy,
           b.UpdatedDateTime,

           lie.ActionDescription,
           lie.ActionId,
           lie.ActionKey,
           lie.BalanceId,
           lie.BatchId,
           lie.CollectionBatchKey,
           lie.CreatedBy,
           lie.CreatedDateTime,
           lie.DatabasePartitionId,
           lie.DeltaDateTime,
           lie.EventActionKey,
           lie.EventActionName,
           lie.EventDateTime,
           lie.EventId,
           lie.EventKey,
           lie.EventName,
           lie.EventTypeDescription,
           lie.EventTypeId,
           lie.EventTypeKey,
           lie.IntegrationBatchKey,
           lie.OrderId,
           lie.OrderLineItemId,
           lie.ReceivedDate,
           lie.RevenueSku,
           lie.RevenueSkuKey,
           lie.RowProcessTime,
           lie.TaskExecutionId,
           lie.TaxIncluded,
           lie.TotalPrice,
           lie.TotalTax,
           lie.UniqueId,
           lie.UpdatedBy,
           lie.UpdatedDateTime,

           bapm.AccountHolderName,
           bapm.AccountHolderNameNormalized,
           bapm.AccountId,
           bapm.AddressId,
           bapm.CollectionBatchKey,
           bapm.CreditCardExpirationDate,
           bapm.CreditCardTypeDescription,
           bapm.CreditCardTypeId,
           bapm.CreditCardTypeKey,
           bapm.DatabasePartitionId,
           bapm.DeltaDateTime,
           bapm.ExternalOwnerId,
           bapm.InitializationVector,
           bapm.InsertedBy,
           bapm.InsertedDateTime,
           bapm.IntegrationBatchKey,
           bapm.Last4AccountNumber,
           bapm.MigratedDateTime,
           bapm.OwnerId,
           bapm.OwnerTypeId,
           bapm.PaymentMethodId,
           bapm.PaymentMethodName,
           bapm.PaymentMethodStatusDescription,
           bapm.PaymentMethodStatusId,
           bapm.PaymentMethodStatusKey,
           bapm.PaymentMethodTypeDescription,
           bapm.PaymentMethodTypeId,
           bapm.PaymentMethodStatusKey,
           bapm.PaymentMethodTypeDescription,
           bapm.PaymentMethodTypeId,
           bapm.PaymentMethodTypeKey,
           bapm.PhoneId,
           bapm.ReceivedDate,
           bapm.RegisteredPaymentMethodId,
           bapm.RowGuid,
           bapm.RowProcessTime,
           bapm.TaskExecutionId,
           bapm.UpdatedBy,
           bapm.UpdatedDateTime,
           
           
           pmt.CollectionBatchKey,
           pmt.DatabasePartitionId,
           pmt.DeltaDateTime,
           pmt.EnforceSupportedActions,
           pmt.IntegrationBatchKey,
           pmt.ISPayinPaymentInstrument,
           pmt.PaymentMethodTypeDescription,
           pmt.PaymentMethodTypeId,
           pmt.PaymentMethodTypeKey,
           pmt.ReceivedDate,
           pmt.RowProcessTime,
           pmt.TaskExecutionId,
           pmt.UpdatedBy,
           pmt.UpdatedDateTime,
           
           
           li.AddressId,
           li.BundleId,
           li.ChargeBeginDate,
           li.ChargeEndDate,
           li.CollectionBatchKey,
           li.CompanyId,
           li.CreatedBy,
           li.CreatedDateTime,
           li.DatabasePartitionId,
           li.DealSubscriptionId,
           li.DeltaDateTime,
           li.ExternalId,
           li.FinanceReasonKey,
           li.IntegrationBatchKey,
           li.ItemInstanceReferenceId,
           li.LineItemExtendedProperty,
           li.LineItemProperty,
           li.LineItemTypeDescription,
           li.LineItemTypeId,
           li.LineItemTypeKey,
           li.NumberOfUnits,
           li.OrderId,
           li.OrderLineItemId,
           li.ProductItemId,
           li.ProductType,
           li.ProductTypeKey,
           li.PurchaseId,
           li.RateGuid,
           li.RateId,
           li.ReceivedDate,
           li.RemitBy,
           li.RemitByFromPropertyXml,
           li.RevenueAllocation,
           li.RevenueSku,
           li.RevenueSkuKey,
           li.RowProcessTime,
           li.SalesModel,
           li.SellerVatId,
           li.ShipFromCountryCode,
           li.SkuCategory,
           li.SkuCategoryKey,
           li.SkuDescription,
           li.SkuName,
           li.SourceTotalPrice,
           li.SourceTotalTax,
           li.TaskExecutionId,
           li.TaxableCountryCode,
           li.TaxIncluded,
           li.TaxSku,
           li.TotalPrice,
           li.TotalTax,
           li.UnitPrice,
           li.UnitTax,
           li.UpdatedBy,
           li.UpdatedDateTime,
           li.Version        
";

            var sr = new StringReader(str);

            string line = string.Empty;
            string result = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    line = line.Replace(',', ' ');
                    var word = line.Split('.');

                    result += line + " AS " + word[0] + "_" + word[1] + ",";

                }
            }

            Console.Write(result);
        }

    }



}
