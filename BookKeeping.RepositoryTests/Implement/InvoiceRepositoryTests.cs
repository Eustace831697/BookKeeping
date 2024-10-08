﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookKeeping.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;



namespace BookKeeping.Repository.Implement.Tests
{
    [TestClass()]
    public class InvoiceRepositoryTests
    {
        private readonly string _ConnectionString = "Server=.;Database=BookKeeping;User Id=bookkeepinguser;Password=ZAQ!2wsx;Trusted_Connection=True;";
        private readonly string _ID = "B1AC8D88-E289-4932-A119-0778AF224362";

        [TestMethod()]
        public void Insert_Test()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);

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

            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void GetCategory_Test()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);

            var result = _invoiceRepository.GetDetailCategoryList();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod()]
        public void GetAll_Test()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);
            InvoiceQueryCondition queryCondition = new InvoiceQueryCondition();

            var result = _invoiceRepository.GetAll(queryCondition);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);

            Invoice invoice = new Invoice
            {
                ID = Guid.Parse(_ID),
                Carrier_Name = "手機條碼2",
                Carrier_Number = "/DUVLOOO",
                Date = DateTime.ParseExact("20240524", "yyyyMMdd", null),
                BAN_of_Seller = "80328000",
                Name_of_Seller = "統一超商股份有限公司桃園縣第OOO分公司",
                Invoice_Number = "AR00008578",
                Amount = 114,
                Invoice_Status = "開立",
                InvoiceDetail = new List<InvoiceDetail> {
                     new InvoiceDetail { Price=59, Product_Name = "醬燒烤雞溏心蛋三明治2" },
                     new InvoiceDetail { Price=55, Product_Name = "拿鐵冰咖啡(大)2" },
                }
            };

            var result = _invoiceRepository.Update(invoice);

            Assert.AreEqual(null, result);
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);
            Guid ID = Guid.Parse(_ID);

            var result = _invoiceRepository.GetByID(ID);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);
            Guid ID = Guid.Parse(_ID);

            var result = _invoiceRepository.Delete(ID);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetMainDataCountTest()
        {
            InvoiceRepository _invoiceRepository = new InvoiceRepository(_ConnectionString);
            InvoiceQueryCondition queryCondition = new InvoiceQueryCondition();

            var result = _invoiceRepository.GetMainDataCount(queryCondition);

            Assert.AreEqual(true, result > -1);
        }
    }
}