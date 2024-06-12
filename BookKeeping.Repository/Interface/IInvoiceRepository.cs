using BookKeeping.Repository.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Interface
{
    public interface IInvoiceRepository
    {
        //寫入
        string Insert(List<Invoice> Invoice);

        //取得列表
        List<InvoiceData> GetList();

        //取得明細分類
        List<InvoiceDetailCategory> GetCategory();  
    }
}
