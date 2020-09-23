using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace smsServ.SmsUtils.Utils
{
    public class httpUtils
    {
        public static bool http_Get(string _url, out string _response, out int _statusCode)
        {
            bool res = false;
            _statusCode = 0;
            _response = string.Empty;
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender1, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(string.Format(_url));
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/json; encoding='utf-8'";
                httpRequest.Timeout = 3 * 60 * 1000;
                string post_response = string.Empty;
                HttpWebResponse osdresponse = (HttpWebResponse)httpRequest.GetResponseWithoutException();
                _statusCode = (int)osdresponse.StatusCode;
                using (StreamReader responseStream = new StreamReader(osdresponse.GetResponseStream()))
                {
                    post_response = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                _response = post_response;
                res = true;
            }
            catch(Exception ex)
            {
                LogWriter._error("HttpGet", ex.ToString());
                res = false;
            }
            return res;
        }
        public static bool http_Post(string _url, string _body, out string _response, out int _statusCode)
        {
            bool res = false;
            _response = string.Empty;
            _statusCode = 0;
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(string.Format(_url));
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json; encoding='utf-8'";
                httpRequest.Timeout = 3 * 60 * 1000;
                byte[] postBytes = Encoding.UTF8.GetBytes(_body);
                httpRequest.ContentLength = postBytes.Length;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                string post_response = string.Empty;
                HttpWebResponse osdresponse = (HttpWebResponse)httpRequest.GetResponseWithoutException();
                _statusCode = (int)osdresponse.StatusCode;
                using (StreamReader responseStream = new StreamReader(osdresponse.GetResponseStream()))
                {
                    post_response = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                _response = post_response;
            }
            catch (Exception ex)
            {
                LogWriter._error("HttpPost", ex.ToString());
                res = false;
            }
            return res;
        }
    }
    public static class WebRequestExtensions
    {
        public static WebResponse GetResponseWithoutException(this WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            try
            {
                return request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                return e.Response;
            }
        }
    }
}
