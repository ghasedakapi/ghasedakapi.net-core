using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ghasedak.Core.Models.Results;

namespace Ghasedak.Core.Interfaces
{
    public interface ISMSService
    {
        Task<SendResult> SendSMSAsync(string message, string receptor, string linenumber = null, DateTime? senddate = null, String checkid = null);
        Task<SendResult> SendSMSAsync(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate = null, string[] checkid = null);
        Task<SendResult> SendSMSAsync(string message, string[] receptor, string linenumber = null, DateTime? senddate = null, string[] checkid = null);
        Task<SendResult> VerifyAsync(int type, string template, string[] receptor, string param1, string param2 = null, string param3 = null, string param4 = null, string param5 = null, string param6 = null, string param7 = null, string param8 = null, string param9 = null, string param10 = null);
        Task<StatusResult> GetStatusAsync(int type, long[] id);
        Task<StatusResult> CancelSMSAsync(long[] messageid);
    }
}
