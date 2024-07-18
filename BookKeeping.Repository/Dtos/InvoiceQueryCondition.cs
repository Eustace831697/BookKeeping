using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Dtos
{
    public class InvoiceQueryCondition
    {
        public string? InvoiceNumber { get; set; }

        public string? ProductName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? StartDateFormat
        {
            get
            {
                if (StartDate.HasValue)
                {
                    return StartDate.Value.ToString("yyyy-MM-dd");
                }
                return null;
            }
        }
        public string? EndDateFormat
        {
            get
            {
                if (EndDate.HasValue)
                {
                    return EndDate.Value.ToString("yyyy-MM-dd");
                }
                return null;
            }
        }


        public int? MinAmount { get; set; }

        public int? MaxAmount { get; set; }
    }
}
