using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Dtos
{
    public class InvoiceDetail
    {
        //發票明細的實體
        [DisplayName("品項名稱")]
        public string Product_Name { get; set; }

        [DisplayName("小計")]
        public int Price { get; set; }

        //非必要
        [DisplayName("分類")]
        public int Category { get; set; }

        [DisplayName("建立時間")]
        public DateTime CreateDate { get; set; }
    }
}
