using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookKeeping.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookKeeping.Repository.Interface;
using BookKeeping.Repository.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace BookKeeping.Repository.Implement.Tests
{
    [TestClass()]
    public class InvoiceRepositoryTests
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceRepositoryTests(IInvoiceRepository invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }

        [TestMethod()]
        public void InsertTest()
        {
            List<Invoice> invoiceGroup = new List<Invoice>();

            invoiceGroup.Add(new Invoice
            {
                Carrier_Name = "手機條碼",
                Carrier_Number = "/DUVLOOO",
                Date = DateTime.ParseExact("20240524", "yyyyMMdd", null),
                BAN_of_Seller = "80328000",
                Name_of_Seller = "統一超商股份有限公司桃園縣第OOO分公司",
                Invoice_Number = "AR00008578",
                Amount = 114,
                Invoice_Status = "開立",
                InvoiceDetail = new List<InvoiceDetail> {
                     new InvoiceDetail { Price=59, Product_Name = "醬燒烤雞溏心蛋三明治" },
                     new InvoiceDetail { Price=55, Product_Name = "拿鐵冰咖啡(大)" },
                }
            });

            invoiceGroup.Add(new Invoice
            {
                Carrier_Name = "手機條碼",
                Carrier_Number = "/DUVLOO1",
                Date = DateTime.ParseExact("20240525", "yyyyMMdd", null),
                BAN_of_Seller = "80328011",
                Name_of_Seller = "統一超商股份有限公司桃園縣第OOO分公司",
                Invoice_Number = "AR00008679",
                Amount = 114,
                Invoice_Status = "開立",
                InvoiceDetail = new List<InvoiceDetail> {
                     new InvoiceDetail { Price=59, Product_Name = "鮪魚肉鬆雙手卷" },
                     new InvoiceDetail { Price=55, Product_Name = "拿鐵冰咖啡(大)" },
                }
            });

            string result = _invoiceRepository.Insert(invoiceGroup);

            Assert.AreEqual("", result);
        }

        [TestMethod()]
        public void InvoiceRepositoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCategoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.Fail();
        }
    }
}

namespace BookKeeping.RepositoryTests4.Implement
{
    internal class InvoiceRepositoryTests
    {
    }
}
