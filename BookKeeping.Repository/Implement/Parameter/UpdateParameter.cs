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
    public class UpdateParameter : IUpdateParameter
    {
        public DynamicParameters AllMainParameter { get; }
        public List<DynamicParameters> AllDetailParameter { get; }

        public UpdateParameter(Invoice invoice)
        {
            AllMainParameter = CreateMainParameter(invoice);
            AllDetailParameter = CreateDetailParameter(invoice.ID, invoice.InvoiceDetail);
        }

        private DynamicParameters CreateMainParameter(Invoice invoice)
        {
            DynamicParameters Parameter = new DynamicParameters();

            Parameter.Add("@ID", invoice.ID, dbType: DbType.Guid, direction: ParameterDirection.Input, size: 38);
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

        private List<DynamicParameters> CreateDetailParameter(Guid ID, List<InvoiceDetail> DetailGroup)
        {
            List<DynamicParameters> ParameterGroup = new List<DynamicParameters>();

            foreach (var Detail in DetailGroup)
            {
                DynamicParameters Parameter = new DynamicParameters();

                Parameter.Add("@Invoice_ID", ID, DbType.Guid);
                Parameter.Add("@Product_Name", Detail.Product_Name, DbType.String, ParameterDirection.Input, 50);
                Parameter.Add("@Price", Detail.Price, DbType.Int32);
                Parameter.Add("@Category", Detail.Category, DbType.Int32);

                ParameterGroup.Add(Parameter);
            }
            return ParameterGroup;
        }
    }
}
