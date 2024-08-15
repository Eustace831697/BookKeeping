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
        
        string Insert(List<Invoice> invoice);

        List<InvoiceData> GetAll(InvoiceQueryCondition invoiceQueryCondition);

        List<InvoiceData> GetByID(Guid ID);

        int GetMainDataCount(InvoiceQueryCondition invoiceQueryCondition);

        string Update(Invoice invoice);

        string Delete(Guid ID);
                
        List<InvoiceDetailCategory> GetDetailCategoryList();
    }
}
