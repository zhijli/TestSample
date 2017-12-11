using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using Microsoft.Commerce.Tools.BatchService.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestSample
{
    using Newtonsoft.Json.Serialization;

    [TestClass]
    public class JsonConvertSample
    {  
        [TestMethod]
        public void JsonConvert_SerializeObject()
        {
            string serializeStr = "{\"Id\":1,\"FirstName\":\"Tom\",\"LastName\":\"Li\"}";
            var order = new Order(1, "Tom", "Li");
            var str = JsonConvert.SerializeObject(order);
            Assert.AreEqual(serializeStr, str);
        }

        [TestMethod]
        public void JsonConvert_DeserializeObject()
        {
            string serializeStr = "{\"Id\":1,\"FirstName\":\"Tom\",\"LastName\":\"Li\"}";
            var expectOrder = new Order(1, "Tom", "Li");
            var order = JsonConvert.DeserializeObject<Order>(serializeStr);
            Assert.AreEqual(expectOrder.Id, order.Id);
            Assert.AreEqual(expectOrder.FirstName, order.FirstName);
            Assert.AreEqual(expectOrder.LastName, order.LastName);
        }

        [TestMethod]
        public void test()
        {
            string serializeStr = "{\\\"Id\\\":1,\\\"Name\\\":\\\"Tom\\\"}";
            Console.Write(serializeStr);
            Console.Write(serializeStr.Replace("\\\"", "\""));
        }

        [TestMethod]
        public void JsonSerialize()
        {
            string serializeStr = "{\"Id\":1,\"FirstName\":\"Tom\",\"LastName\":\"Li\",}";
            var expectOrder = new Order(1, "Tom" ,"Li");
            var serializer = new JsonSerializer();
            using (var sw = new StringWriter())
            using (var jw = new JsonTextWriter(sw))
            {
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Formatting = Formatting.Indented;
                serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Full;
                serializer.Serialize(jw,expectOrder);
                Console.WriteLine(sw.ToString());
            }
        }

        [TestMethod]
        public void JsonSerialize1()
        {
            string serializeStr = "{\"c\":[{\"cp\":true,\"n\":\"engineering\",\"pn\":\"BatchReadTest\",\"t\":[{\"n\":\"ScsTestConnection\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"MintTestConnection\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"PgwTestConnection\",\"a\":[{\"n\":\"PGW\"}]},{\"n\":\"ModernAccountTestConnection\",\"a\":[{\"n\":\"Modern\"}]},{\"n\":\"MDollarTestConnection\",\"a\":[{\"n\":\"MDollar\"}]},{\"n\":\"ModernPaymentTestConnection\",\"a\":[{\"n\":\"Modern\"}]},{\"n\":\"ExecScripts\",\"a\":[{\"n\":\"Utility\"}]}]},{\"cp\":false,\"n\":\"subscription\",\"t\":[{\"dn\":\"CancelSubs\",\"n\":\"CancelSubscription\",\"t\":\"CancelSubscription,DeactivateSubscription,AzureCancelSubscription\",\"e\":\"CancelSubs\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"O365\",\"t\":\"DeactivateSubscription\"},{\"n\":\"AOO\",\"t\":\"AzureCancelSubscription\"}]},{\"dn\":\"ConvertSubs\",\"n\":\"ConvertSubscription\",\"t\":\"ConvertSubscription,AzureConvertSubscription,CTPCommerceConvertSubscription,OmsConvertSubscription\",\"e\":\"ConvertSubs\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"MINT\",\"t\":\"CTPCommerceConvertSubscription\"},{\"n\":\"AOO\",\"t\":\"AzureConvertSubscription\"},{\"n\":\"O365\",\"t\":\"OmsConvertSubscription\"}]},{\"dn\":\"UpdateSubsInfo\",\"n\":\"UpdateSubscriptionInfo\",\"t\":\"UpdateSubscriptionInfo,AzureUpdateSubscriptionInfo,UpdateSubscriptionIncludedQuantity,MintUpdateSubscription\",\"e\":\"UpdateSubsInfo\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"MINT\",\"t\":\"MintUpdateSubscription\"},{\"n\":\"AOO\",\"t\":\"AzureUpdateSubscriptionInfo\"},{\"n\":\"O365\",\"t\":\"UpdateSubscriptionIncludedQuantity\"}]},{\"dn\":\"ReinstateSubs\",\"n\":\"ReinstateSubscription\",\"t\":\"ReinstateSubscription,ReactivateSubscription,AzureReinstateSubscription\",\"e\":\"ReinstateSubs\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"O365\",\"t\":\"ReactivateSubscription\"},{\"n\":\"AOO\",\"t\":\"AzureReinstateSubscription\"}]},{\"n\":\"ProvisionServices\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"DeprovisionServices\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"AdminExtendSubs\",\"n\":\"AdminExtendSubscription\",\"e\":\"AdminExtendSubs\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"AdjustSubs\",\"n\":\"AdjustSubscription\",\"e\":\"AdjustSubs\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"ConvertSubsEx\",\"n\":\"ConvertSubscriptionEx\",\"e\":\"ConvertSubsEx\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"ApplySubsDiscount\",\"n\":\"ApplySubscriptionDiscount\",\"e\":\"ApplySubsDiscount\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"AdminCancelScheduledRenewal\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"AdjustMonetaryCommitment...\",\"n\":\"AdjustMonetaryCommitmentDiscount\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"ExtendSubs\",\"n\":\"AdjustSubscriptionEndDate\",\"a\":[{\"n\":\"O365\"}]},{\"n\":\"EscheatSettlementAmount\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"UpdateSubscriptionPartnerOfRec...\",\"n\":\"UpdateSubscriptionPartnerOfRecord\",\"t\":\"UpdateSubscriptionPartnerOfRecord, AzureUpdateSubscriptionPartnerOfRecord\",\"a\":[{\"n\":\"O365\"},{\"n\":\"AOO\",\"t\":\"AzureUpdateSubscriptionPartnerOfRecord\"}]},{\"n\":\"AddViolation\",\"t\":\"AddViolation,AzureAddViolation\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"AOO\",\"t\":\"AzureAddViolation\"}]},{\"n\":\"RemoveViolation\",\"t\":\"RemoveViolation,AzureRemoveViolation\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"AOO\",\"t\":\"AzureRemoveViolation\"}]},{\"dn\":\"AdjustMonetaryCommitment...\",\"n\":\"AzureAdjustMonetaryCommitmentDiscountRate\",\"e\":\"AdjustMonetaryCommitmentDiscountRate\",\"a\":[{\"n\":\"AOO\"}]},{\"dn\":\"DecreaseMonetaryCommitment...\",\"n\":\"AzureDecreaseMonetaryCommitmentAmount\",\"e\":\"DecreaseMonetaryCommitmentAmount\",\"a\":[{\"n\":\"AOO\"}]},{\"dn\":\"ManageOrder\",\"n\":\"AzureManageOrder\",\"a\":[{\"n\":\"AOO\"}]},{\"dn\":\"RenewSubs\",\"n\":\"AzureRenewSubscription\",\"a\":[{\"n\":\"AOO\"}]},{\"dn\":\"TransferSubs\",\"n\":\"AzureTransferSubscription\",\"a\":[{\"n\":\"AOO\"}]},{\"dn\":\"GenerateSubsLockoutCode\",\"n\":\"GenerateSubscriptionLockoutCode\",\"a\":[{\"n\":\"O365\"}]},{\"dn\":\"CancelSubsLockoutCode\",\"n\":\"CancelSubscriptionLockoutCode\",\"a\":[{\"n\":\"O365\"}]},{\"dn\":\"ManageAgents\",\"n\":\"MintManageAgents\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"CancelRecurrence\",\"a\":[{\"n\":\"MV\"}]},{\"n\":\"RefundRecurrence\",\"a\":[{\"n\":\"MV\"}]},{\"n\":\"UpdateRecurrence\",\"a\":[{\"n\":\"MV\"}]},{\"dn\":\"UpdateRecurrencePI\",\"n\":\"UpdateRecurrencePaymentInstrument\",\"e\":\"UpdateRecurrencePI\",\"a\":[{\"n\":\"MV\"}]},{\"n\":\"TurnOffSubscriptionAutoRenew\",\"a\":[{\"n\":\"O365\"}]}]},{\"cp\":false,\"n\":\"token\",\"t\":[{\"n\":\"ActivateToken\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"BlacklistToken\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"DeactivateToken\",\"a\":[{\"n\":\"SCS\"}]}]},{\"cp\":false,\"n\":\"account\",\"t\":[{\"n\":\"AddComment\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"AddRoleAssignment\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"CloseAccount\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"RemoveRoleAssignment\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"UpdateAccountInfo\",\"t\":\"UpdateAccountInfo,SaveTenantProfile,AzureUpdateAccountInfo,ModernUpdateAccountInfo,MintUpdateAccount\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"O365\",\"t\":\"SaveTenantProfile\"},{\"n\":\"AOO\",\"t\":\"AzureUpdateAccountInfo\"},{\"n\":\"Modern\",\"t\":\"ModernUpdateAccountInfo\"},{\"n\":\"MINT\",\"t\":\"MintUpdateAccount\"}]},{\"n\":\"UpdateTaxId\",\"t\":\"UpdateTaxId,AzureUpdateTaxId\",\"a\":[{\"n\":\"O365\"},{\"n\":\"AOO\",\"t\":\"AzureUpdateTaxId\"}]}]},{\"cp\":false,\"n\":\"payment\",\"t\":[{\"dn\":\"CreditPIEx3\",\"n\":\"CreditPaymentInstrumentEx3\",\"e\":\"CreditPIEx3\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"RemovePI\",\"n\":\"RemovePaymentInstrument\",\"t\":\"RemovePaymentInstrument,ModernRemovePaymentInstrument,OmsRemovePaymentInstrument\",\"e\":\"RemovePI\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"Modern\",\"t\":\"ModernRemovePaymentInstrument\"},{\"n\":\"OMS\",\"t\":\"OmsRemovePaymentInstrument\"}]},{\"dn\":\"SwitchPI\",\"n\":\"SwitchPaymentInstrument\",\"t\":\"SwitchPaymentInstrument,AzureSwitchPaymentInstrument\",\"e\":\"SwitchPI\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"AOO\",\"t\":\"AzureSwitchPaymentInstrument\"}]},{\"dn\":\"CreditPIEx2\",\"n\":\"CreditPaymentInstrumentEx2\",\"e\":\"CreditPIEx2\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"StoredValueManagePromotional...\",\"n\":\"StoredValueManagePromotionalProgram\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"StoredValueManagePromotional...\",\"n\":\"StoredValueManagePromotionalSkuLimit\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"BanPI\",\"n\":\"BanPaymentInstrument\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"UnbanPI\",\"n\":\"UnbanPaymentInstrument\",\"a\":[{\"n\":\"SCS\"}]},{\"dn\":\"UpdatePI\",\"n\":\"CTUpdatePaymentInstrument\",\"t\":\"CTUpdatePaymentInstrument,MintUpdatePaymentInstrument\",\"a\":[{\"n\":\"CT\"},{\"n\":\"MINT\",\"t\":\"MintUpdatePaymentInstrument\"}]},{\"n\":\"PayModCharge\",\"a\":[{\"n\":\"Modern\"}]}]},{\"cp\":false,\"n\":\"billing\",\"t\":[{\"n\":\"OffsetLineItem\",\"t\":\"OffsetLineItem,MDollarOffsetLineItem\",\"a\":[{\"n\":\"SCS\"},{\"n\":\"MDollar\",\"t\":\"MDollarOffsetLineItem\"}]},{\"n\":\"RefundOrder\",\"a\":[{\"n\":\"MDollar\"}]},{\"n\":\"ArbitraryRefund\",\"a\":[{\"n\":\"MDollar\"}]},{\"n\":\"CloseBalance\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"RefundTax\",\"t\":\"RefundTax,ModernRefundTax\",\"a\":[{\"n\":\"MINT\"},{\"n\":\"Modern\",\"t\":\"ModernRefundTax\"}]},{\"n\":\"RefundMonetaryReserve\",\"a\":[{\"n\":\"Skype\"}]},{\"n\":\"SettleBalance\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"TransferBalance\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"WriteOffBalance\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"ManagePreApprovalList\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"IssueReceipt\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"RefundPurchase\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"SettlePhysicalGoods\",\"n\":\"CTPCommerceSettlePhysicalGoods\",\"e\":\"SettlePhysicalGoods\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"PurchaseOfferingEx2\",\"a\":[{\"n\":\"SCS\"}]},{\"n\":\"ManagePreApproval\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"IncreaseMonetaryCreditAmount\",\"n\":\"AzureIncreaseMonetaryCreditAmount\",\"e\":\"IncreaseMonetaryCreditAmount\",\"a\":[{\"n\":\"AOO\"}]},{\"n\":\"SetCreditCheckThreshold\",\"a\":[{\"n\":\"MINT\"}]},{\"n\":\"UpdateBalanceInfo\",\"a\":[{\"n\":\"MINT\"}]},{\"dn\":\"RecalculateTax\",\"n\":\"ModernRecalculateTax\",\"e\":\"RecalculateTax\",\"a\":[{\"n\":\"Modern\"}]},{\"n\":\"AddCredit\",\"a\":[{\"n\":\"Skype\"}]}]}],\"p\":[{\"dn\":\"SCS\",\"n\":\"SCS\",\"ds\":\"SCS APIs. Work for most of the partners like Xbox, Windows, etc.\"},{\"dn\":\"OMS - O365\",\"n\":\"O365\",\"ds\":\"OMS ordering and support APIs. Work for O365, Intune and CRM on subscription/account related scenarios.\"},{\"dn\":\"OMS - Azure\",\"n\":\"AOO\",\"ds\":\"OMS restful APIs. Work for AOO (Azure on OMS) on subscription/account related scenarios.\"},{\"dn\":\"MINT\",\"n\":\"MINT\",\"ds\":\"Mint APIs. Work for most of the partners like Xbox, Windows, etc.\"},{\"dn\":\"MV\",\"n\":\"MV\",\"ds\":\"Membership View restful API. Work for Membership View Services.\"},{\"dn\":\"Modern\",\"n\":\"Modern\",\"ds\":\"Modern restful API. Work for Modern Account Service on account related scenarios.\"},{\"dn\":\"OMS\",\"n\":\"OMS\",\"ds\":\"OMS APIs. Work for AOO (Azure on OMS) and O365 related scenarios.\"},{\"dn\":\"CT\",\"n\":\"CT\",\"ds\":\"CT APIs. Work for most of the partners like Xbox, Windows, etc.\"},{\"dn\":\"MDollar\",\"n\":\"MDollar\",\"ds\":\"MDollar restful API. Work for MDollar on refund related scenarios.\"},{\"dn\":\"OMS - Skype\",\"n\":\"Skype\",\"ds\":\"Skype restful API. Work for Skype Monetary Reserve related scenarios.\"},{\"dn\":\"PGW\",\"n\":\"PGW\",\"ds\":\"PGW APIs. Work for most of the partners like Xbox, Windows, etc.\"},{\"dn\":\"Utility\",\"n\":\"Utility\",\"ds\":\"Batch tool system utility\"}]}";
            var tc= JsonConvert.DeserializeObject<TaskListConfig>(serializeStr);
            int count = 1;
            foreach (var category in tc.Categories)
            {
                foreach (var task in category.Tasks)
                {
                    if (task.Templates == null)
                    {
                        Console.WriteLine("ID: {0}{3}CategoryName: {1}{3}Name: {2}", count++, category.Name, task.Name, "\t");
                    }
                    else
                    {
                        var templates = task.Templates.Split(',');
                        foreach (var template in templates)
                        {
                            Console.WriteLine("ID: {0}{3}CategoryName: {1}{3}Name: {2}", count++, category.Name, template, "\t");
                        }
                    }

                }
            }
        }

        [TestMethod]
        public void JsonSerialize_To_lowerCamelCase()
        {
            var expectOrder = new Order(1, "Tom", "Li");
            var setting = new JsonSerializerSettings();
            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var str = JsonConvert.SerializeObject(expectOrder, setting);
            Console.Write(str);
        }
    }

    class Order
    {
        public Order(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public int Id;
        public string FirstName;
        public string LastName;
    }


}
