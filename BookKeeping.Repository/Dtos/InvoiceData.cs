using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Dtos
{
    public class InvoiceData
    {
        //從資料庫直接取出的資料格式

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


        [DisplayName("建立日期")]
        public DateTime InvoiceCreateDate { get; set; }

        [DisplayName("主檔GUID")]
        public Guid Invoice_ID { get; set; }

        [DisplayName("品項名稱")]
        public string Product_Name { get; set; }

        [DisplayName("小計")]
        public int Price { get; set; }

        //非必要
        [DisplayName("分類編號")]
        public int Category { get; set; }

        [DisplayName("分類")]
        public string CategoryName { get; set; }

        [DisplayName("建立時間")]
        public DateTime DetailCreateDate { get; set; }
    }
}
