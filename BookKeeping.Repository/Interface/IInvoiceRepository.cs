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
        //新增
        string Insert(List<Invoice> invoice);

        //存取
        List<InvoiceData> GetAll();

        //更新
        string Update(Invoice invoice);

        //取得明細分類
        List<InvoiceDetailCategory> GetCategory();
    }
}
