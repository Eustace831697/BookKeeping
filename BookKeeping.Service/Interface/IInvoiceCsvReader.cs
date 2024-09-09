using BookKeeping.Repository.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Service.Interface
{
    public interface IInvoiceCsvReader
    {
        public List<Invoice> ConvertToInvoice(IFormFileCollection csvFiles);
    }
}
