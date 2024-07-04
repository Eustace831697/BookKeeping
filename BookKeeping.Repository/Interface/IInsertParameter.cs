using BookKeeping.Repository.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Interface
{
    public interface IInsertParameter
    {
        List<DynamicParameters> AllMainParameters { get; }

        List<DynamicParameters> AllDetailParameters { get; }
    }
}
