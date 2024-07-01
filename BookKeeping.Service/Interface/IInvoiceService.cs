using BookKeeping.Repository.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;





namespace BookKeeping.Service.Interface
{
    public interface IInvoiceService
    {
        //新增發票
        string Add(List<Invoice> Invoice);

        //更新發票資訊
        int Update();

        //刪除整張發票
        int Delete();

        //取得發票資訊
        List<Invoice> GetAll();

        //讀取每月紀錄
        List<Invoice> ReadCSV(IFormFileCollection files);

        //取得發票明細分類
        List<SelectListItem> getCategory();
    }
}
