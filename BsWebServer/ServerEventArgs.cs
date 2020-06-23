using BsWebServer.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Events
{
    public class ServerEventArgs : EventArgs
    {
        public HttpResponse resp;
        public HttpRequest req;

        public ServerEventArgs(HttpResponse resp, HttpRequest req)
        {
            this.req = req;
            this.resp = resp;
        }
    }
}
