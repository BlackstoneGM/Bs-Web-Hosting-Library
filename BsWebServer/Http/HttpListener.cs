using BsWebServer.Http.Events;
using BsWebServer.Http.Handling;
using BsWebServer.Variables;
using System;
using System.Collections.Generic;
using System.IO;
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
            WebServerRequest += HttpServerhandler;
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
            server.Stop();
        }

        public void Start()
        {
            if(ip == "*")
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                listenthread.Start();
                watchthread.Start();
            }
            else
            {
                server = new TcpListener(IPAddress.Parse(ip), port);
                server.Start();
                listenthread.Start();
                watchthread.Start();
            }
        }

        protected virtual void OnWebServerRequest(HttpServerEventArgs se)
        {
            WebServerRequest?.Invoke(this, se);
        }

        public void useStatic(DirectoryInfo dir)
        {
            staticdirectory.Add(dir);
        }

        private List<DirectoryInfo> staticdirectory = new List<DirectoryInfo>();

        private void HttpServerhandler(object sender, HttpServerEventArgs se)
        {
            if(staticdirectory.Count == 0)
            {
                return;
            }
            else
            {
                foreach(DirectoryInfo directoryInfo in staticdirectory)
                {
                    if(File.Exists(Path.Combine(directoryInfo.FullName, se.req.location)))
                    {
                        se.resp.httpversion = se.req.httpversion;
                        se.resp.status = StatusCodes.OK;
                        se.resp.contenttype = MimeTypes.getFileMimeType(Path.Combine(directoryInfo.FullName, se.req.location));
                        byte[] bytesresponse = File.ReadAllBytes(Path.Combine(directoryInfo.FullName, se.req.location));
                        se.resp.content = bytesresponse;
                        se.resp.contentlength = bytesresponse.Length;

                        se.resp.closeResponse();
                    }
                }
            }
        }

    }
}