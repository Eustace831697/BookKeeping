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
        //SQL連線字串
        private readonly string _ConnectionString;

        //DI
        public InvoiceRepository(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
        }

        /// <summary>
        /// 取得明細分類
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<InvoiceDetailCategory> getCategory()
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

        /// <summary>
        /// 寫入發票資料
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        public string Insert(List<Invoice> Invoice)
        {
            string rtn = "";
            try
            {
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open(); //連線
                        foreach (var item in Invoice)
                        {
                            DynamicParameters para_Main = new DynamicParameters();

                            //主資料參數
                            para_Main.Add("@Carrier_Name", item.Carrier_Name, DbType.String, ParameterDirection.Input, 50);
                            para_Main.Add("@Carrier_Number", item.Carrier_Number, DbType.String, ParameterDirection.Input, 30);
                            para_Main.Add("@Date", item.Date, DbType.DateTime);
                            para_Main.Add("@BAN_of_Seller", item.BAN_of_Seller, DbType.String, ParameterDirection.Input, 8);
                            para_Main.Add("@Name_of_Seller", item.Name_of_Seller, DbType.String, ParameterDirection.Input, 50);
                            para_Main.Add("@Invoice_Number", item.Invoice_Number, DbType.String, ParameterDirection.Input, 10);
                            para_Main.Add("@Amount", item.Amount, DbType.Int32);
                            para_Main.Add("@Invoice_Status", item.Invoice_Status, DbType.String, ParameterDirection.Input, 10);

                            //回傳參數
                            para_Main.Add("@InvoiceID", dbType: DbType.String, direction: ParameterDirection.Output, size: 38);

                            //寫入並回傳ID
                            conn.Execute("Insert_Invoice", para_Main, commandType: CommandType.StoredProcedure);
                            string ID = para_Main.Get<string>("@InvoiceID");

                            //明細資料參數
                            List<DynamicParameters> para_Detail = new List<DynamicParameters>();

                            foreach (var Detail in item.InvoiceDetail)
                            {
                                DynamicParameters para = new DynamicParameters();
                                para.Add("@Invoice_ID", ID, DbType.String);
                                para.Add("@Product_Name", Detail.Product_Name, DbType.String, ParameterDirection.Input, 50);
                                para.Add("@Price", Detail.Price, DbType.Int32);
                                para.Add("@Category", Detail.Category, DbType.Int32);

                                para_Detail.Add(para);
                            }
                            //寫入
                            conn.Execute("Insert_Invoice_Detail", para_Detail, commandType: CommandType.StoredProcedure);
                        }
                    }
                    Transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn = ex.ToString();
            }
            return rtn;
        }

        /// <summary>
        /// 取得所有紀錄 暫無條件
        /// </summary>
        /// <returns></returns>
        public List<Invoice> getList()
        {
            try
            {
                //連線
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    conn.Open();
                    //交易
                    using (var Transaction = conn.BeginTransaction())
                    {
                        conn.Query<Invoice>
                    }

                }
    
            }
            catch (Exception ex)
            {

            }
        }
    }

}


