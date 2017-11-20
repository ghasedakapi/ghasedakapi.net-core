using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhasedakApi.Core.Exceptions
{
    public class GhasedakApiException:Exception
    {
        /// <summary>
        /// Create an empty GhasedakApiException
        /// </summary>
        public GhasedakApiException() { }

        /// <summary>
        /// Create a GhasedakApiException from an error message
        /// </summary>
        /// <param name="message">Error message</param>
        public GhasedakApiException(string message) : base(message) { }

        /// <summary>
        /// Create a GhasedakApiException from message and another exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="exception">Original Exception</param>
        public GhasedakApiException(string message, Exception exception) : base(message, exception) { }
    }
}