using BookingKeeping.Common.Implement;
using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Implement;
using BookKeeping.Repository.Interface;
using BookKeeping.Service.Dtos;
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
            InvoiceCsvReader invoiceCsvReader = new InvoiceCsvReader();

            List<Invoice> invoiceList = invoiceCsvReader.ConvertToInvoice(files);

            return invoiceList;
        }

        public List<SelectListItem> getCategory()
        {
            //取得分類
            var CategoryList = _invoiceRepository.GetDetailCategoryList();

            //將各分類加入下拉選項
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var item in CategoryList)
            {
                selectListItems.Add(new SelectListItem() { Text = item.Category_Name, Value = item.Category.ToString() });
            }

            return selectListItems;
        }

        public InvoiceResult GetAll(InvoiceQueryCondition QueryCondition)
        {
            try
            {
                List<InvoiceData> InvoiceDataGroup = _invoiceRepository.GetAll(QueryCondition);
                int MainDataCount = _invoiceRepository.GetMainDataCount(QueryCondition);

                InvoiceDataManager invoiceDataManager = new InvoiceDataManager();

                InvoiceResult invoiceResult = new InvoiceResult();
                invoiceResult.invoiceGroup = invoiceDataManager.ConvertToInvoice(InvoiceDataGroup);
                invoiceResult.PaginationCount = (int)Math.Ceiling((double)MainDataCount / (double)QueryCondition.DisplayCount);

                return invoiceResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Invoice> GetByID(Guid ID)
        {
            List<InvoiceData> invoiceData = _invoiceRepository.GetByID(ID);

            InvoiceDataManager invoiceDataManager = new InvoiceDataManager();
            return invoiceDataManager.ConvertToInvoice(invoiceData);
        }

        public string Delete(Guid ID)
        {
            return _invoiceRepository.Delete(ID);
        }
    }
}
