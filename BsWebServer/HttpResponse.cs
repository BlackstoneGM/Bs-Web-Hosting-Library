using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Handling
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
        public int status;
        public double contentlength;
        public string contenttype;
        public string content;

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
            if(status == 200)
            {
                statussign = "OK";
            }
            else
            {
                status = 200;
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
    }
}
