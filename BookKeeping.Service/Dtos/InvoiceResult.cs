using BookKeeping.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Service.Dtos
{
    public class InvoiceResult
    {
        public List<Invoice> invoiceGroup {  get; set; }

        public int PaginationCount { get; set; } = 1;
    }
}
