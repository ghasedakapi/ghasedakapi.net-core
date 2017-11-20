using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GhasedakApi.Core.Models.Results;

namespace GhasedakApi.Core.Interfaces
{
  public interface IVoiceService
    {
        SendResult SendVoice(string message,string [] receptor,string senddate);
    }
}
