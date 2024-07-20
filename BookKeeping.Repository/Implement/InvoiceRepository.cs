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
using BookingKeeping.Common.Implement;
using BookingKeeping.Common.Interface;

namespace BookKeeping.Repository.Implement
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly string _ConnectionString;

        private IErrorLog _ErrorLog;

        public InvoiceRepository(string ConnectionString, IErrorLog errorLog=null)
        {
            this._ConnectionString = ConnectionString;
            this._ErrorLog = errorLog;
        }

        public string Insert(List<Invoice> InvoiceGroup)
        {
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<InvoiceData> GetAll(InvoiceQueryCondition QueryCondition)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();

                    QueryParameter queryParameter = new QueryParameter(QueryCondition);

                    return conn.Query<InvoiceData>("[dbo].[Get_Invoice_Data]", queryParameter.Parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                _ErrorLog.WriterLog(ex.ToString());
                return null;
            }
        }

        public List<InvoiceData> GetByID(Guid ID)
        {
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();

                    return conn.Query<InvoiceData>("[dbo].[Get_Invoice_Data_By_ID]", new { ID = ID }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {                
                _ErrorLog.WriterLog(ex.ToString());
                return null;
            }
        }

        public string Update(Invoice invoice)
        {
            try
            {
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        UpdateParameter updateParameter = new UpdateParameter(invoice);

                        conn.Execute("Update_Invoice", updateParameter.AllMainParameter, commandType: CommandType.StoredProcedure);
                        conn.Execute("Delete_Invoice_Detail_Only", new { ID = invoice.ID }, commandType: CommandType.StoredProcedure);
                        conn.Execute("Insert_Invoice_Detail", updateParameter.AllDetailParameter, commandType: CommandType.StoredProcedure);
                    }
                    Transaction.Complete();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<InvoiceDetailCategory> GetDetailCategoryList()
        {
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();

                    return conn.Query<InvoiceDetailCategory>("SELECT [Category],[Category_Name] fROM Invoice_Detail_Category").ToList();
                }
            }
            catch (Exception ex)
            {
                string Exception = ex.ToString();
                _ErrorLog.WriterLog(Exception);

                return null;
            }
        }

        public string Delete(Guid ID)
        {
            try
            {
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        conn.Execute("Delete_Invoice", new { ID = ID }, commandType: CommandType.StoredProcedure);
                    }
                    Transaction.Complete();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}


