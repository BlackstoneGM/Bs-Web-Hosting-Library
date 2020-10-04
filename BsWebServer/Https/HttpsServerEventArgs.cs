using BsWebServer.Https.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Https.Events
{
    public class HttpsServerEventArgs : EventArgs
    {
        public HttpsResponse resp;
        public HttpsRequest req;

        public HttpsServerEventArgs(HttpsResponse resp, HttpsRequest req)
        {
            this.req = req;
            this.resp = resp;
        }
    }
}
