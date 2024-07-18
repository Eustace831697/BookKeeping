using BookKeeping.Repository.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Implement
{
    public class QueryParameter
    {
        private DynamicParameters _ParameterGroup;

        public QueryParameter(InvoiceQueryCondition QueryCondition)
        {          
            _ParameterGroup = CreateParameter(QueryCondition);
        }

        private DynamicParameters CreateParameter(InvoiceQueryCondition QueryCondition)
        {
            DynamicParameters ParameterGroup = new DynamicParameters();

            ParameterGroup.Add("@InvoiceNumber", QueryCondition.InvoiceNumber, DbType.String, ParameterDirection.Input);
            ParameterGroup.Add("@ProductName", QueryCondition.ProductName, DbType.String, ParameterDirection.Input);
            ParameterGroup.Add("@StartDate",  QueryCondition.StartDate, DbType.DateTime2, ParameterDirection.Input);
            ParameterGroup.Add("@EndDate", QueryCondition.EndDate, DbType.DateTime2, ParameterDirection.Input);
            ParameterGroup.Add("@MinAmount", QueryCondition.MinAmount, DbType.Int32, ParameterDirection.Input);
            ParameterGroup.Add("@MaxAmount", QueryCondition.MaxAmount, DbType.Int32, ParameterDirection.Input);

            return ParameterGroup;
        }

        public DynamicParameters GetParameters()
        {
            return _ParameterGroup;
        }
    }
}
