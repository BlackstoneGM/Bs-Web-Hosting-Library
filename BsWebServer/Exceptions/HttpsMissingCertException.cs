using System;
using System.Collections.Generic;
using System.Text;

namespace BsWebServer.Exceptions
{
    public class HttpsMissingCertException : Exception
    {
        public HttpsMissingCertException()
        {

        }

        public HttpsMissingCertException(string message)
            : base(message)
        {

        }
    }
}
