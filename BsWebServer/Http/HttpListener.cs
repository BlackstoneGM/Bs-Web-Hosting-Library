using BsWebServer.Http.Events;
using BsWebServer.Http.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BsWebServer.Http.Listeners
{
    public class HttpListener
    {
        public event EventHandler<HttpServerEventArgs> WebServerRequest;

        string ip;
        int port;
        Thread listenthread;
        Thread watchthread;


        Byte[] bytes = new Byte[1024];
        String data = null;

        TcpListener server = null;

        public bool isRunning;

        public HttpListener(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            listenthread = new Thread(Listen);
            watchthread = new Thread(Watch);
        }

        void Listen()
        {
            
            try
            {
                isRunning = true;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;
                    i = stream.Read(bytes, 0, bytes.Length);
                    Console.WriteLine(System.Text.Encoding.ASCII.GetString(bytes, 0, i));

                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    HttpRequest httpRequest = new HttpRequest(data);

                    HttpResponse httpResponse = new HttpResponse(stream, client);

                    OnWebServerRequest(new HttpServerEventArgs(httpResponse, httpRequest));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Stop();
                isRunning = false;
            }
        }

        void Watch()
        {
            isRunning = true;
            while (listenthread.IsAlive)
            {
                Thread.Sleep(100);
            }
            isRunning = false;
        }

        public void Start()
        {
            server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            listenthread.Start();
            watchthread.Start();
        }

        protected virtual void OnWebServerRequest(HttpServerEventArgs se)
        {
            WebServerRequest?.Invoke(this, se);
        }
    }
}
