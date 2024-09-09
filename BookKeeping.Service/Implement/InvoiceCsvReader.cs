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
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.ToUpper()[0] == 'M')
                        {                            
                            Invoice aa = ConvertToMainData(line);
                            
                            InvoiceList.Add(aa);    //加入發票主檔
                        }
                        else if (line.ToUpper()[0] == 'D')
                        {
                            InvoiceDetail invoiceDetail = ConvertToDetailData(line);

                            //把這筆明細資料附加至指定發票主檔
                            var targetMainData = InvoiceList.FirstOrDefault(x => (x.Invoice_Number == invoiceDetail.Invoice_Name));
                            if (targetMainData != null)
                            {
                                targetMainData.InvoiceDetail.Add(invoiceDetail);
                            }
                        }
                    }
                }
            }
            return InvoiceList;
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
                                                    //
            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Invoice_Name = DetailInfo[1]; //發票號碼 識別要塞回哪個主檔
            invoiceDetail.Price = Convert.ToInt32(DetailInfo[2]);   //小計
            invoiceDetail.Product_Name = DetailInfo[3]; //品項名稱
            invoiceDetail.Category = invoiceDetail.Price <= 0 ? -1 : 0; //價格負數=折扣(=-1) 其他先未分類(=0)

            return invoiceDetail;
        }
    }
}
