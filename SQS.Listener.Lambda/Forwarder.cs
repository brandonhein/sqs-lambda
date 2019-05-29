using Amazon.Lambda.SQSEvents;
using System;
using System.IO;
using System.Net;

namespace SQS.Listener.Lambda
{
    public static class Forwarder
    {
        public static HttpStatusCode ForwardTo(this SQSEvent.SQSMessage message, string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = message.Body.GetContentType();

            using (var stream = new StreamWriter(webRequest.GetRequestStream()))
            {
                stream.Write(message.Body);
                stream.Flush();
                stream.Close();
            }

            try
            {
                var response = (HttpWebResponse)webRequest.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return response.StatusCode;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var response = (HttpWebResponse)ex.Response;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        return response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
