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
        string Add(List<Invoice> Invoice);
   
        string Update(Invoice invoice);
     
        int Delete();
  
        List<Invoice> GetAll();

        List<Invoice> GetByID(Guid ID);

        List<Invoice> ReadCSV(IFormFileCollection files);

        List<SelectListItem> getCategory();
    }
}
