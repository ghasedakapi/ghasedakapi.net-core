using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GhasedakApi.Core.Models.Results;

namespace GhasedakApi.Core.Interfaces
{
   public  interface IReceiveService
    {
        Task<ReceiveMessageResult> ReceiveList(string linenumber, int isRead);
    }
}
