using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Implement;
using BookKeeping.Repository.Interface;
using BookKeeping.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;




namespace BookKeeping.Service.Implement
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }

        public string Add(List<Invoice> Invoice)
        {
            string rtn = _invoiceRepository.Insert(Invoice);

            return rtn;
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public string Update(Invoice invoice)
        {
            string rtn = _invoiceRepository.Update(invoice);

            return rtn;
        }


        // 讀取CSV並回傳資料
        public List<Invoice> ReadCSV(IFormFileCollection files)
        {            
            List<Invoice> InvoiceList = new List<Invoice>();

            //逐個檔案讀取
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.ToUpper()[0] == 'M')
                        {
                            Invoice invoice = new Invoice();
                            invoice.InvoiceDetail = new List<InvoiceDetail>();
                            string[] mainInfo = line.Split('|');    //分割

                            invoice.Carrier_Name = mainInfo[1];     //載具名稱
                            invoice.Carrier_Number = mainInfo[2];   //載具號碼
                            invoice.Date = DateTime.ParseExact(mainInfo[3], "yyyyMMdd", null);    //載具號碼
                            invoice.BAN_of_Seller = mainInfo[4];    //商店統編
                            invoice.Name_of_Seller = mainInfo[5];   //發票號碼
                            invoice.Invoice_Number = mainInfo[6];   //發票號碼 
                            invoice.Amount = Convert.ToInt32(mainInfo[7]);  //總金額
                            invoice.Invoice_Status = mainInfo[8];   //發票狀態

                            //加入發票主檔
                            InvoiceList.Add(invoice);
                        }
                        else if (line.ToUpper()[0] == 'D')
                        {
                            InvoiceDetail invoiceDetail = new InvoiceDetail();

                            string[] DetailInfo = line.Split('|');  //分割
                            string Detail_Invoice_Number = DetailInfo[1];  //發票號碼 識別要塞回哪個主檔

                            invoiceDetail.Price = Convert.ToInt32(DetailInfo[2]);   //小計
                            invoiceDetail.Product_Name = DetailInfo[3]; //品項名稱
                            invoiceDetail.Category = invoiceDetail.Price <= 0 ? -1 : 0; //價格負數=折扣(=-1) 其他先未分類(=0)

                            //把這筆明細資料附加至指定發票主檔
                            var main = InvoiceList.FirstOrDefault(x => (x.Invoice_Number == Detail_Invoice_Number));
                            if (main != null)
                            {
                                main.InvoiceDetail.Add(invoiceDetail);
                            }
                        }
                    }
                }
            }
            return InvoiceList;
        }

        /// <summary>
        /// 取得發票明細分類
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<SelectListItem> getCategory()
        {
            //取得分類
            var CategoryList = _invoiceRepository.GetCategory();

            //將各分類加入下拉選項
            List<SelectListItem> selectListItems = new List<SelectListItem>();            
            foreach (var item in CategoryList)
            {
                selectListItems.Add(new SelectListItem() { Text = item.Category_Name, Value = item.Category.ToString() });
            }

            return selectListItems;
        }

        /// <summary>
        /// 取得資料並加入model
        /// </summary>
        /// <returns></returns>
        public List<Invoice> GetAll()
        {
            List<InvoiceData> invoiceData = _invoiceRepository.GetAll();

            InvoiceDataManager invoiceDataManager = new InvoiceDataManager();

            return invoiceDataManager.ConvertToInvoice(invoiceData);
        }

        public List<Invoice> GetByID(Guid ID)
        {
            List<InvoiceData> invoiceData = _invoiceRepository.GetByID(ID);

            InvoiceDataManager invoiceDataManager = new InvoiceDataManager();

            return invoiceDataManager.ConvertToInvoice(invoiceData);

            return null;
        }
    }
}
