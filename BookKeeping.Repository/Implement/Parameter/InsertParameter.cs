using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Implement.Parameter
{
    public class InsertParameter : IInsertParameter
    {
        public List<DynamicParameters> AllMainParameters { get; }
        public List<DynamicParameters> AllDetailParameters { get; }

        public InsertParameter(List<Invoice> InvoiceGroup)
        {
            AllMainParameters = new List<DynamicParameters>();
            AllDetailParameters = new List<DynamicParameters>();

            CreateInvoiceParameters(InvoiceGroup);
        }

        private void CreateInvoiceParameters(List<Invoice> InvoiceGroup)
        {
            //一張發票會有一個主檔+多筆明細資料
            //分開處理後各別加入主檔及明細的List並回傳
            //每張發票使用同一組Guid紀錄關聯
            foreach (Invoice invoice in InvoiceGroup)
            {
                Guid ID = Guid.NewGuid();

                DynamicParameters MainParameters = CreateMaintParameter(ID, invoice);
                List<DynamicParameters> DetailParameters = CreateDetailtParameter(ID, invoice.InvoiceDetail);

                AllMainParameters.Add(MainParameters);
                AllDetailParameters.AddRange(DetailParameters);
            }
        }

        private DynamicParameters CreateMaintParameter(Guid ID, Invoice invoice)
        {
            DynamicParameters Parameter = new DynamicParameters();

            Parameter.Add("@ID", ID, dbType: DbType.Guid, direction: ParameterDirection.Input, size: 38);
            Parameter.Add("@Carrier_Name", invoice.Carrier_Name, DbType.String, ParameterDirection.Input, 50);
            Parameter.Add("@Carrier_Number", invoice.Carrier_Number, DbType.String, ParameterDirection.Input, 30);
            Parameter.Add("@Date", invoice.Date, DbType.DateTime);
            Parameter.Add("@BAN_of_Seller", invoice.BAN_of_Seller, DbType.String, ParameterDirection.Input, 8);
            Parameter.Add("@Name_of_Seller", invoice.Name_of_Seller, DbType.String, ParameterDirection.Input, 50);
            Parameter.Add("@Invoice_Number", invoice.Invoice_Number, DbType.String, ParameterDirection.Input, 10);
            Parameter.Add("@Amount", invoice.Amount, DbType.Int32);
            Parameter.Add("@Invoice_Status", invoice.Invoice_Status, DbType.String, ParameterDirection.Input, 10);

            return Parameter;
        }

        private List<DynamicParameters> CreateDetailtParameter(Guid ID, List<InvoiceDetail> DetailGroup)
        {
            List<DynamicParameters> DetailParameters = new List<DynamicParameters>();

            foreach (var Detail in DetailGroup)
            {
                DynamicParameters Parameter = new DynamicParameters();

                Parameter.Add("@Invoice_ID", ID, DbType.Guid);
                Parameter.Add("@Product_Name", Detail.Product_Name, DbType.String, ParameterDirection.Input, 50);
                Parameter.Add("@Price", Detail.Price, DbType.Int32);
                Parameter.Add("@Category", Detail.Category, DbType.Int32);

                DetailParameters.Add(Parameter);
            }
            return DetailParameters;
        }
    }
}
