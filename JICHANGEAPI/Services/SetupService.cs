using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Services.Companies;
using JichangeApi.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace JichangeApi.Services
{
    public class SetupService
    {
        INVOICE innn = new INVOICE();
        private readonly RequesterService requesterService = new RequesterService();
        private readonly CompanyBankService companyBankService = new CompanyBankService();

        public ItemListModel GetLatestTransactionsPayments(HashSet<long> companyId,int monthsBefore) 
        {
            try
            {
                DateTime endDate = DateTime.Now; // Today's date
                DateTime startDate = endDate.AddMonths((-monthsBefore));
                List<Payment> payments = new Payment().GetStatusPassedPaymentsWithin(startDate, endDate, companyId);
                return new ItemListModel { Name = "Transactions", Statistic = payments.Count.ToString() };
            }
            catch (Exception ex)
            {
                return new ItemListModel { Name = "Transactions", Statistic = "0" };
            }
        }

        /*public string GetControlNumberPaymentDetails(string controlNumber)
        {
            var url = "http://183.83.33.156:90/Payment/api/PaymentDetails/GetDetails";
            var data = new
            {
                paymentReference = controlNumber
            };
            
        }

        public long GetVendorOverviewTransactionsStat(long vendorId)
        {
            try
            {
                int monthsBefore = 1;
                var payments = GetLatestTransactionsPayments(vendorId, monthsBefore);
                foreach (var payment in payments)
                {
                    var details = GetControlNumberPaymentDetails(payment.Control_No);
                }
            }
            catch (NullReferenceException ex)
            {
                return 0;
            }
        }*/

        /*public ItemListModel GetTransactionsOverview(HashSet<long> companies, int monthsBefore)
        {

        }*/

        public void GetBankerOverview(long branchId)
        {
            try
            {
                List<CompanyBankMaster> companies = companyBankService.GetCompaniesByBranch(branchId);
                HashSet<long> companyIds = companies.Select(e => e.CompSno).ToHashSet();
                ItemListModel transactions = GetLatestTransactionsPayments(companyIds, 1);

            }
            catch(NullReferenceException ex)
            {

            }
        }
    }
}
