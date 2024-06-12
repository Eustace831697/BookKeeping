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


        public string Insert(List<Invoice> InvoiceGroup)
        {
            string rtn = "";
            try
            {
                using (var Transaction = new TransactionScope())
                {
                    using (var conn = new SqlConnection(_ConnectionString))
                    {
                        conn.Open(); 
                        foreach (Invoice invoice in InvoiceGroup)
                        {
                            Guid ID = Guid.NewGuid();

                            DynamicParameters mainParameters = CreateMaintParameters(ID, invoice);
                            conn.Execute("Insert_Invoice", mainParameters, commandType: CommandType.StoredProcedure);

                            List<DynamicParameters> detailParameters = CreateDetailtParameters(ID, invoice.InvoiceDetail);
                            conn.Execute("Insert_Invoice_Detail", detailParameters, commandType: CommandType.StoredProcedure);
                        }
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

        private DynamicParameters CreateMaintParameters(Guid ID, Invoice invoice)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ID", ID, dbType: DbType.Guid, direction: ParameterDirection.Input, size: 38);
            parameters.Add("@Carrier_Name", invoice.Carrier_Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Carrier_Number", invoice.Carrier_Number, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@Date", invoice.Date, DbType.DateTime);
            parameters.Add("@BAN_of_Seller", invoice.BAN_of_Seller, DbType.String, ParameterDirection.Input, 8);
            parameters.Add("@Name_of_Seller", invoice.Name_of_Seller, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Invoice_Number", invoice.Invoice_Number, DbType.String, ParameterDirection.Input, 10);
            parameters.Add("@Amount", invoice.Amount, DbType.Int32);
            parameters.Add("@Invoice_Status", invoice.Invoice_Status, DbType.String, ParameterDirection.Input, 10);

            return parameters;
        }

        private List<DynamicParameters> CreateDetailtParameters(Guid ID, List<InvoiceDetail> DetailGroup)
        {
            List<DynamicParameters> allDetailParameter = new List<DynamicParameters>();

            foreach (var Detail in DetailGroup)
            {
                DynamicParameters Parameter = new DynamicParameters();

                Parameter.Add("@Invoice_ID", ID, DbType.Guid);
                Parameter.Add("@Product_Name", Detail.Product_Name, DbType.String, ParameterDirection.Input, 50);
                Parameter.Add("@Price", Detail.Price, DbType.Int32);
                Parameter.Add("@Category", Detail.Category, DbType.Int32);

                allDetailParameter.Add(Parameter);
            }
            return allDetailParameter;
        }

        /// <summary>
        /// 取得所有紀錄 暫無條件
        /// </summary>
        /// <returns></returns>
        public List<InvoiceData> GetList()
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
                string rtn = ex.ToString();
                rtn.ToString();
            }
            return InvoiceDataList;
        }
    }

}


