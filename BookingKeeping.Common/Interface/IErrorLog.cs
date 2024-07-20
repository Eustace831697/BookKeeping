using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingKeeping.Common.Interface
{
    public interface IErrorLog
    {
        void WriterLog(string Content);
    }
}
