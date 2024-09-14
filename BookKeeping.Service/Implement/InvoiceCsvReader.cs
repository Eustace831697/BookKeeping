using BookKeeping.Repository.Dtos;
using BookKeeping.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Service.Implement
{
    public class InvoiceCsvReader : IInvoiceCsvReader
    {
        public List<Invoice> ConvertToInvoice(IFormFileCollection csvFiles)
        {
            List<Invoice> InvoiceList = new List<Invoice>();

            foreach (var file in csvFiles)
            {
                ReadFileAndUpdateInvoiceList(file, InvoiceList);
            }
            return InvoiceList;
        }

        private void ReadFileAndUpdateInvoiceList(IFormFile file, List<Invoice> InvoiceList)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    UpdateInvoiceListWithType(line, InvoiceList);
                }
            }
        }

        private void UpdateInvoiceListWithType(string line, List<Invoice> InvoiceList)
        {
            char type = line.ToUpper()[0];

            if (type == 'M')
            {
                Invoice MainData = ConvertToMainData(line);
                InvoiceList.Add(MainData);    //加入發票主檔
            }
            else if (type == 'D')
            {
                InvoiceDetail DetailData = ConvertToDetailData(line);
                AddDetailDataToTargetMainData(DetailData, InvoiceList); //明細加入至對應發票主檔
            }
        }

        private Invoice ConvertToMainData(string line)
        {
            string[] mainInfo = line.Split('|');    //分割

            Invoice invoice = new Invoice();
            invoice.Carrier_Name = mainInfo[1];     //載具名稱
            invoice.Carrier_Number = mainInfo[2];   //載具號碼
            invoice.Date = DateTime.ParseExact(mainInfo[3], "yyyyMMdd", null);    //載具號碼
            invoice.BAN_of_Seller = mainInfo[4];    //商店統編
            invoice.Name_of_Seller = mainInfo[5];   //發票號碼
            invoice.Invoice_Number = mainInfo[6];   //發票號碼 
            invoice.Amount = Convert.ToInt32(mainInfo[7]);  //總金額
            invoice.Invoice_Status = mainInfo[8];   //發票狀態

            return invoice;
        }

        private InvoiceDetail ConvertToDetailData(string line)
        {
            string[] DetailInfo = line.Split('|');  //分割

            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Invoice_Name = DetailInfo[1]; //發票號碼
            invoiceDetail.Price = Convert.ToInt32(DetailInfo[2]);   //小計
            invoiceDetail.Product_Name = DetailInfo[3]; //品項名稱
            invoiceDetail.Category = invoiceDetail.Price <= 0 ? -1 : 0; //價格負數=折扣(=-1) 其他先未分類(=0)

            return invoiceDetail;
        }

        private void AddDetailDataToTargetMainData(InvoiceDetail invoiceDetail, List<Invoice> InvoiceList)
        {
            var targetMainData = InvoiceList.FirstOrDefault(x => (x.Invoice_Number == invoiceDetail.Invoice_Name));

            if (targetMainData != null) targetMainData.InvoiceDetail.Add(invoiceDetail);
        }
    }
}
