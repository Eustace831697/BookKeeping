using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Dtos
{
    public class Invoice
    {      
        public Guid ID { get; set; }

        [DisplayName("載具名稱")]
        public string Carrier_Name { get; set; }

        [DisplayName("載具號碼")]
        public string Carrier_Number { get; set; }

        [DisplayName("發票日期")]
        public DateTime Date { get; set; }

        [DisplayName("商店統編")]
        public string BAN_of_Seller { get; set; }

        [DisplayName("商店店名")]
        public string Name_of_Seller { get; set; }

        [DisplayName("發票號碼")]
        public string Invoice_Number { get; set; }

        [DisplayName("總金額")]
        public int Amount { get; set; }

        [DisplayName("發票狀態")]
        public string Invoice_Status { get; set; }

        [DisplayName("資料建立日期")]
        public DateTime CreateDate { get; set; }


        //發票明細
        [DisplayName("發票明細")]
        public List<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
