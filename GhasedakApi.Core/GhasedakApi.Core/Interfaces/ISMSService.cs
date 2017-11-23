using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GhasedakApi.Core.Models.Results;

namespace GhasedakApi.Core.Interfaces
{
    public interface ISMSService
    {
        Task<SendResult> SendSMS(string message, string linenumber, string receptor);
        Task<SendResult> SendSMS(string message, string linenumber, string receptor, DateTime senddate);
        Task<SendResult> SendSMS(string message, string linenumber, string[] receptor);
        Task<SendResult> SendSMS(string message, string linenumber, string[] receptor, DateTime senddate);
        Task<SendResult> SendSMS(string[] message, string[] linenumber, string[] receptor);
        Task<SendResult> SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate);
        Task<SendResult> Verify(int type, string template, string[] receptor, string param1, string param2, string param3);
        Task<StatusResult> GetStatus(long[] messageid);
        Task<SendResult> CancelSMS(long[] messageid);
        Task<SelectMessageResult> SelectSMS(long[] messageid);
    }
}
