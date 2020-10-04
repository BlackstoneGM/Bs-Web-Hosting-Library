using BsWebServer.Http.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Http.Events
{
    public class HttpServerEventArgs : EventArgs
    {
        public HttpResponse resp;
        public HttpRequest req;

        public HttpServerEventArgs(HttpResponse resp, HttpRequest req)
        {
            this.req = req;
            this.resp = resp;
        }
    }
}
