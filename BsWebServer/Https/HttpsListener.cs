using BsWebServer.Https.Events;
using BsWebServer.Https.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BsWebServer.Https.Listeners
{
    public class HttpsListener
    {
        public event EventHandler<HttpsServerEventArgs> WebServerRequest;

        string ip;
        int port;
        Thread listenthread;
        Thread watchthread;


        Byte[] bytes1 = new Byte[1];
        Byte[] bytes2 = new Byte[1023];
        Byte[] bytes = new Byte[1024];
        String data = null;

        public X509Certificate2 x509Certificate = new X509Certificate2();

        TcpListener server = null;

        public bool isRunning;

        public HttpsListener(string ip, int port)
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
                if (x509Certificate.GetRawCertData().Length < 1)
                {
                    throw new BsWebServer.Exceptions.HttpsMissingCertException("Missing SSL Certificate In Https Listener");
                }
                else
                {
                    try
                    {
                        isRunning = true;
                        while (true)
                        {
                            TcpClient client = server.AcceptTcpClient();

                            data = null;

                            SslStream sslStream = new SslStream(client.GetStream());

                            sslStream.AuthenticateAsServer(x509Certificate, false, System.Security.Authentication.SslProtocols.Default, false);

                            bytes1 = new Byte[1];
                            bytes2 = new Byte[1023];
                            bytes = new Byte[1024];

                            sslStream.Read(bytes1, 0, bytes1.Length);
                            while (0 == sslStream.Read(bytes2, 0, bytes2.Length))
                            {
                                Thread.Sleep(50);
                            }

                            bytes[0] = bytes1[0];
                            bytes2.CopyTo(bytes, 1);

                            data = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            Console.WriteLine(System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length));

                            HttpsRequest httpRequest = new HttpsRequest(data);

                            HttpsResponse httpResponse = new HttpsResponse(sslStream, client);

                            OnWebServerRequest(new HttpsServerEventArgs(httpResponse, httpRequest));
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
            }
            catch(Exception e)
            {
                throw new BsWebServer.Exceptions.HttpsMissingCertException("Missing SSL Certificate In Https Listener");
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

        protected virtual void OnWebServerRequest(HttpsServerEventArgs se)
        {
            WebServerRequest?.Invoke(this, se);
        }
    }
}
