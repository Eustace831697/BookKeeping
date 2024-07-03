using BookKeeping.Repository.Dtos;
using BookKeeping.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Implement
{
    public class InsertParameter : IInsertParameter
    {
        private List<DynamicParameters> _MainParameters = new List<DynamicParameters>();
        private List<DynamicParameters> _DetailParameters = new List<DynamicParameters>();

        public List<DynamicParameters> MainParameters
        {
            get { return _MainParameters; }
        }
        public List<DynamicParameters> DetailParameters
        {
            get { return _DetailParameters; }
        }

        public InsertParameter(List<Invoice> InvoiceGroup)
        {
            CreateInvoiceParameters(InvoiceGroup);
        }

        private void CreateInvoiceParameters(List<Invoice> InvoiceGroup)
        {
            foreach (Invoice invoice in InvoiceGroup)
            {
                Guid ID = Guid.NewGuid();

                DynamicParameters MainParameters = CreateMaintParameters(ID, invoice);
                List<DynamicParameters> DetailParameters = CreateDetailtParameters(ID, invoice.InvoiceDetail);

                _MainParameters.Add(MainParameters);
                _DetailParameters.AddRange(DetailParameters);
            }
        }

        private DynamicParameters CreateMaintParameters(Guid ID, Invoice invoice)
        {
            DynamicParameters MainParameters = new DynamicParameters();

            MainParameters.Add("@ID", ID, dbType: DbType.Guid, direction: ParameterDirection.Input, size: 38);
            MainParameters.Add("@Carrier_Name", invoice.Carrier_Name, DbType.String, ParameterDirection.Input, 50);
            MainParameters.Add("@Carrier_Number", invoice.Carrier_Number, DbType.String, ParameterDirection.Input, 30);
            MainParameters.Add("@Date", invoice.Date, DbType.DateTime);
            MainParameters.Add("@BAN_of_Seller", invoice.BAN_of_Seller, DbType.String, ParameterDirection.Input, 8);
            MainParameters.Add("@Name_of_Seller", invoice.Name_of_Seller, DbType.String, ParameterDirection.Input, 50);
            MainParameters.Add("@Invoice_Number", invoice.Invoice_Number, DbType.String, ParameterDirection.Input, 10);
            MainParameters.Add("@Amount", invoice.Amount, DbType.Int32);
            MainParameters.Add("@Invoice_Status", invoice.Invoice_Status, DbType.String, ParameterDirection.Input, 10);

            return MainParameters;
        }

        private List<DynamicParameters> CreateDetailtParameters(Guid ID, List<InvoiceDetail> DetailGroup)
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
