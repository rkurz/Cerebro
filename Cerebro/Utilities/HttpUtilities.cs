using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace Cerebro.Utilities
{
    public class HttpUtilities
    {
        public static string HttpGet(string url)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            StreamReader reader;
            string data;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = new NetworkCredential("rkurz", "password");
            //request.Headers.Add(HttpRequestHeader.Accept, "application/json");
            request.Accept = "application/json";
            //request.Headers.Add(HttpRequestHeader.Authorization, "Basic");
            response = request.GetResponse() as HttpWebResponse;
            reader = new StreamReader(response.GetResponseStream());
            data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return data;
        }
    }
}