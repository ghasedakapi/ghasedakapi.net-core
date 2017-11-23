using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GhasedakApi.Core.Models.Results;

namespace GhasedakApi.Core.Interfaces
{
  public interface IVoiceService
    {
      Task<SendResult> SendVoice(string message,string [] receptor,string senddate);
    }
}
