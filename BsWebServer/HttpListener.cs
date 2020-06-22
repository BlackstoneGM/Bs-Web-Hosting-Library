using BsWebServer.Events;
using BsWebServer.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Listeners
{
    public class HttpListener
    {
        public event EventHandler<ServerEventArgs> WebServerRequest;

        string ip;
        int port;
        public HttpListener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Listen()
        {
            TcpListener server = null;

            server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();

            Byte[] bytes = new Byte[512];
            String data = null;

            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;
                    i = stream.Read(bytes, 0, bytes.Length);

                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    HttpRequest httpRequest = new HttpRequest(data);

                    HttpResponse httpResponse = new HttpResponse(stream, client);

                    OnWebServerRequest(new ServerEventArgs(httpResponse, httpRequest));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Stop();
            }
        }

        protected virtual void OnWebServerRequest(ServerEventArgs se)
        {
            WebServerRequest?.Invoke(this, se);
        }
    }
}
