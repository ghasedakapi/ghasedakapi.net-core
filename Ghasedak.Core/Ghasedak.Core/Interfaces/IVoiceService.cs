using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ghasedak.Core.Models.Results;

namespace Ghasedak.Core.Interfaces
{
  public interface IVoiceService
    {
        Task<SendResult> SendVoice(string message, string[] receptor, DateTime? senddate = null);
    }
}
