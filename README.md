<h2>Bs Web Server</h2>

[![pipeline status](https://gitlab.bsstudios.net/bs-studios/bs-web-hosting-library/badges/master/pipeline.svg)](https://gitlab.bsstudios.net/bs-studios/bs-web-hosting-library/-/commits/master)


A Library Designed to Handle The Main Load For Making A Custom Web Server

***

<h3>Example For Basic Http Server</h3>

    static void Main(string[] args)
    {
        HttpListener httpListener = new HttpListener("*", 8080);
        httpListener.WebServerRequest += HttpServerhandler;
        httpListener.Start();
        while (httpsListener.isRunning)
        {
            Thread.Sleep(100);
        }

    }

    public static void HttpServerhandler(object sender, HttpServerEventArgs se)
    {
        se.resp.httpversion = se.req.httpversion;
        se.resp.status = 200;
        se.resp.contenttype = MimeTypes.HTML;
        string html = "<h1>This Is An Example</h1>";
        se.resp.content = html;
        byte[] bytesresponse = Encoding.ASCII.GetBytes(html);
        se.resp.contentlength = bytesresponse.Length;

        se.resp.closeResponse();
    }

***

<h3>Example For Basic Https Server<h3>

Please note, the current version of this library only supports pfx bundle certificates.

    static void Main(string[] args)
    {
        HttpsListener httpsListener = new HttpsListener("*", 443);
        httpsListener.WebServerRequest += HttpsServerhandler;
        httpsListener.x509Certificate.Import("(path to certificate)", "(password for certificate)", X509KeyStorageFlags.MachineKeySet);
        httpsListener.Start();

        while (httpsListener.isRunning)
        {
            Thread.Sleep(100);
        }

    }

    public static void HttpsServerhandler(object sender, HttpsServerEventArgs se)
    {
        se.resp.httpversion = se.req.httpversion;
        se.resp.status = 200;
        se.resp.contenttype = MimeTypes.HTML;
        string html = "<h1>This Is An Example</h1>";
        se.resp.content = html;
        byte[] bytesresponse = Encoding.ASCII.GetBytes(html);
        se.resp.contentlength = bytesresponse.Length;

        se.resp.closeResponse();
    }
