using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Http.Handling
{
    public class HttpResponse
    {
        /*
            HTTP/1.1 200 OK
            Content-Length: 662
            Content-Type: text/html
            Server: BSServer
            Date: Sat, 30 May 2020 00:35:32 GMT
        */

        public string httpversion;
        public BsWebServer.Variables.StatusCodes status;
        public double contentlength;
        public string contenttype;
        public string content;

        public Dictionary<string, Cookie> cookies = new Dictionary<string, Cookie>();

        NetworkStream stream;
        TcpClient client;

        public HttpResponse(NetworkStream stream, TcpClient client)
        {
            this.stream = stream;
            this.client = client;
        }

        public override string ToString()
        {
            string statussign;
            if(status == BsWebServer.Variables.StatusCodes.OK)
            {
                statussign = "OK";
            }
            else
            {
                status = BsWebServer.Variables.StatusCodes.OK;
                statussign = "OK";
            }

            return $"{httpversion.Remove(8)} {status} {statussign}\nContent-Length: {contentlength}\nContent-Type: {contenttype}\nServer: BSServer\nData: {DateTime.UtcNow}\n\n{content}";
        }

        public void closeResponse()
        {
            byte[] finalbytes = Encoding.ASCII.GetBytes(this.ToString());

            stream.Write(finalbytes, 0, finalbytes.Length);
            stream.Flush();
            stream.Close();
            client.Close();
        }

        public void addCookie(Cookie cookie)
        {
            cookies.Add(cookie.Name ,cookie);
        }
    }
}
