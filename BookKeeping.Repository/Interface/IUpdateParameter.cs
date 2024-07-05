using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Repository.Interface
{
    public interface IUpdateParameter
    {
        DynamicParameters AllMainParameter { get; }

        List<DynamicParameters> AllDetailParameter { get; }
    }
}
