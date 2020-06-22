using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BsWebServer.Addresses
{
    public class HostAddress
    {
        public string IpAddress;
        public string Port;

        public HostAddress(string IpAddress, string Port)
        {
            this.IpAddress = IpAddress;
            this.Port = Port;
        }

        public HostAddress(string IpAddress)
        {
            this.IpAddress = IpAddress;
            this.Port = "";
        }

        public static HostAddress Parse(string stAddress)
        {
            string[] temp = stAddress.Split(':');
            if (temp.Length > 1)
            {
                return new HostAddress(temp[0], temp[1]);
            }
            else
            {
                return new HostAddress(temp[0]);
            }
        }

        public override string ToString()
        {
            if (Port == String.Empty)
            {
                return IpAddress;
            }
            else
            {
                return IpAddress + ":" + Port;
            }
        }

        public IPAddress ToIPAddress()
        {
            return IPAddress.Parse(IpAddress);
        }
    }
}
