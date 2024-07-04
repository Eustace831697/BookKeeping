using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace BookKeeping.Repository.Implement
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly string _ConnectionString;

        public InvoiceRepository(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
        }
        
        public string Insert(List<Invoice> InvoiceGroup)
        {
            string rtn = null;            
            try
            {
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        InsertParameter insertParameter = new InsertParameter(InvoiceGroup);

                        conn.Execute("Insert_Invoice", insertParameter.AllMainParameters, commandType: CommandType.StoredProcedure);
                        conn.Execute("Insert_Invoice_Detail", insertParameter.AllDetailParameters, commandType: CommandType.StoredProcedure);
                    }
                    Transaction.Complete();
                    return rtn;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<InvoiceDetailCategory> GetCategory()
        {
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();
                    var Category = conn.Query<InvoiceDetailCategory>("SELECT [Category],[Category_Name] fROM Invoice_Detail_Category");
                    return Category.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<InvoiceDetailCategory>();
            }
        }
        
        public List<InvoiceData> GetAll()
        {
            List<InvoiceData> InvoiceDataList = new List<InvoiceData>();

            try
            {
                //連線
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();
                    InvoiceDataList = conn.Query<InvoiceData>("[dbo].[Get_Invoice_Data]", commandType: CommandType.StoredProcedure).ToList();

                    return InvoiceDataList;
                }
            }
            catch (Exception ex)
            {
                return InvoiceDataList;
            }
        }
    }

}


