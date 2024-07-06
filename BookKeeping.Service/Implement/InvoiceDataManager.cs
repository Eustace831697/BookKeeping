using BookKeeping.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookKeeping.Service.Implement
{
    public class InvoiceDataManager
    {
        public List<Invoice> ConvertToInvoice(List<InvoiceData> invoiceDataGroup)
        {
            List<InvoiceData> Distinct_MainDataGroup = invoiceDataGroup.GroupBy(x => x.ID).Select(group => group.First()).ToList();
            List<Invoice> invoiceGroup = CreateMainData(Distinct_MainDataGroup);

            foreach (Invoice invoice in invoiceGroup)
            {
                List<InvoiceData> DetailDataGroup = invoiceDataGroup.Where(x => x.Invoice_ID == invoice.ID).ToList();
                invoice.InvoiceDetail = CreateDetailData(DetailDataGroup);
            }
            return invoiceGroup;
        }

        private List<Invoice> CreateMainData(List<InvoiceData> MainDataGroup)
        {
            List<Invoice> InvoiceGroup = new List<Invoice>();

            foreach (InvoiceData MainData in MainDataGroup)
            {
                Invoice invoice = new Invoice();

                invoice.ID = MainData.ID;
                invoice.Carrier_Name = MainData.Carrier_Name;
                invoice.Carrier_Number = MainData.Carrier_Number;
                invoice.Date = MainData.Date;
                invoice.BAN_of_Seller = MainData.BAN_of_Seller;
                invoice.Name_of_Seller = MainData.Name_of_Seller;
                invoice.Invoice_Number = MainData.Invoice_Number;
                invoice.Amount = MainData.Amount;
                invoice.Invoice_Status = MainData.Invoice_Status;
                invoice.CreateDate = MainData.InvoiceCreateDate;

                InvoiceGroup.Add(invoice);
            }
            return InvoiceGroup;
        }

        private List<InvoiceDetail> CreateDetailData(List<InvoiceData> DetailGroup)
        {
            List<InvoiceDetail> invoiceDetail = new List<InvoiceDetail>();

            foreach (var Detail in DetailGroup)
            {
                invoiceDetail.Add(new InvoiceDetail()
                {
                    Product_Name = Detail.Product_Name,
                    Price = Detail.Price,
                    Category = Detail.Category,
                    CategoryName = Detail.CategoryName
                });
            }
            return invoiceDetail;
        }
    }
}
