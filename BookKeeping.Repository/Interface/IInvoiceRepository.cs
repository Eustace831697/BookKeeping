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
        
        List<InvoiceData> GetAll();

        List<InvoiceData> GetByID(Guid ID);
        
        string Update(Invoice invoice);

        string Delete(Guid ID);
                
        List<InvoiceDetailCategory> GetDetailCategoryList();
    }
}
