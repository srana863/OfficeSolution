using Layer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layer.Model.Enums;

namespace Layer.Business.Interfaces
{
    public interface IReportManager
    {
        DataTable GetTransactionReport(int TransactionId);
       
    }
}
