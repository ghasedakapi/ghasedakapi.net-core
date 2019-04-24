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
        Task<ReceiveMessageResult> ReceiveList(string linenumber, int isRead);
        Task<ReceiveMessageResult> ReceiveListPaging(string linenumber, int isRead, DateTime fromdate, DateTime todate, int page = 0, int offset = 200);
    }
}
