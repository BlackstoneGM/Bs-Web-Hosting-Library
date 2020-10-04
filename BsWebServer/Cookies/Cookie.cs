using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BsWebServer.Cookies
{
    public class Cookie
    {
        public DateTime experation;
        public int MaxeAge;
        public string Domain;
        public string Path;
        public string Name;
        public string Content;

        public Cookie()
        {

        }

        public Cookie(string name, string content)
        {
            this.Content = content;
            this.Name = name;
        }

        public override string ToString()
        {
            string finalstring = "Set-Cookie: ";
            if(Name == null || Name == "")
            {
                return "";
            }
            else
            {
                finalstring += Name + "=";
            }
            if(Content == null|| Name == "")
            {
                return "";
            }
            else
            {
                finalstring += Content + "; ";
            }
            if(experation != null)
            {
                finalstring += "Expires=" + experation.ToString() + "; ";
            }
            if(Domain != null)
            {
                finalstring += "Domain=" + Domain + "; ";
            }
            if(Path != null)
            {
                finalstring += "Path " + Path + "; ";
            }
            return finalstring;
        }
    }
}
