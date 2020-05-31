using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ghasedak.Core.Models.Results;

namespace Ghasedak.Core.Interfaces
{
   public  interface IReceiveService
    {
        Task<ReceiveMessageResult> ReceiveListAsync(string linenumber, int isRead, string dep = null);
        Task<ReceiveMessageResult> ReceiveListPagingAsync(string linenumber, int isRead, DateTime fromdate, DateTime todate, int page = 0, int offset = 200, string dep = null);
    }
}
