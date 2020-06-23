using BsWebServer.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Handling
{
    public class HttpRequest
    {
        public string requesttype;
        public string location;
        public string httpversion;
        public HostAddress host;
        public string connectiontype;
        public string useragent;

        public HttpRequest(string httprawrequest)
        {
            string[] splitrequest = httprawrequest.Split('\n');
            for(int i = 0; i < splitrequest.Length; i++)
            {
                if (splitrequest[i].StartsWith("GET"))
                {
                    requesttype = "GET";
                    string[] temp = splitrequest[i].Split(' ');
                    location = temp[1];
                    httpversion = temp[2];
                }

                if (splitrequest[i].StartsWith("Host"))
                {
                    string temp = splitrequest[i].Remove(0, 6);
                    host = HostAddress.Parse(temp);

                }

                if (splitrequest[i].StartsWith("Connection"))
                {
                    string temp = splitrequest[i].Remove(0, 12);
                    connectiontype = temp;
                }

                if (splitrequest[i].StartsWith("User-Agent"))
                {
                    string temp = splitrequest[i].Remove(0, 12);
                    useragent = temp;
                }
            }
        }
    }
}
