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
            try
            {
                int MainCount = 0, DetailCount = 0;

                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        InsertParameter insertParameter = new InsertParameter(InvoiceGroup);

                        MainCount = conn.Execute("Insert_Invoice", insertParameter.AllMainParameters, commandType: CommandType.StoredProcedure);
                        DetailCount = conn.Execute("Insert_Invoice_Detail", insertParameter.AllDetailParameters, commandType: CommandType.StoredProcedure);

                        if (MainCount > 0 && DetailCount > 0)
                        {
                            Transaction.Complete();
                        }
                        else
                        {
                            return "寫入資料數量錯誤";
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<InvoiceData> GetAll()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();
                    return conn.Query<InvoiceData>("[dbo].[Get_Invoice_Data]", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
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
                return null;
            }
        }

        public string Update(Invoice invoice)
        {
            try
            {
                int UpdateCount = 0, DeleteDetailCount = 0, InsertDetailCount = 0;

                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        UpdateParameter updateParameter = new UpdateParameter(invoice);

                        UpdateCount = conn.Execute("Update_Invoice", updateParameter.AllMainParameter, commandType: CommandType.StoredProcedure);
                        DeleteDetailCount = conn.Execute("Delete_Invoice_Detail_Only", new { ID = invoice.ID }, commandType: CommandType.StoredProcedure);
                        InsertDetailCount = conn.Execute("Insert_Invoice_Detail", updateParameter.AllDetailParameter, commandType: CommandType.StoredProcedure);
                    }

                    if (UpdateCount > 0 && DeleteDetailCount > 0 && InsertDetailCount > 0)
                    {
                        Transaction.Complete();
                        return null;
                    }
                    else
                    {
                        return "更新資料數量錯誤";
                    }
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
                return null;
            }
        }

        public string Delete(Guid ID)
        {
            try
            {
                int DeleteCount = 0;
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open();

                        DeleteCount = conn.Execute("Delete_Invoice", new { ID = ID }, commandType: CommandType.StoredProcedure);
                    }
                    if (DeleteCount > 0)
                    {
                        Transaction.Complete();
                        return null;
                    }
                    else
                    {
                        return "刪除資料數量錯誤";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }

}


