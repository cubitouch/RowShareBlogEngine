using System;
using System.Configuration;
using System.Globalization;
using System.Net;

namespace RowShare.Api
{
    public class RowShareCommunication
    {
        protected static string Cookie { get; set; }
        protected const string CookieName = ".RSAUTH";
        protected const string RowShareLoginConfigKey = "RowShareLogin";
        protected const string RowSharePasswordConfigKey = "RowSharePassword";
        protected const string RowShareUrl = "https://www.rowshare.com/";

        public static string GetData(string url, string login = null, string pwd = null)
        {
            SetCredentials(login, pwd);
            using (var client = new WebClient())
            {
                if (!String.IsNullOrEmpty(Cookie))
                {
                    client.Headers.Add(HttpRequestHeader.Cookie, String.Format("{0}={1}", CookieName, Cookie));
                }
                return client.DownloadString(String.Format(CultureInfo.CurrentCulture, "{0}api/{1}", RowShareUrl, url));
            }
        }

        /// <summary>
        /// Set Credentials.
        /// </summary>
        /// <param name="login">If not set, get the AppSettings key defined by RowShareLoginConfigKey</param>
        /// <param name="pwd">If not set, Get the AppSettings key defined by RowSharePasswordConfigKey</param>
        private static void SetCredentials(string login = null, string pwd = null)
        {
            if (String.IsNullOrEmpty(Cookie))
            {
                if (String.IsNullOrEmpty(login))
                    login = ConfigurationManager.AppSettings[RowShareLoginConfigKey];
                if (String.IsNullOrEmpty(pwd))
                    pwd = ConfigurationManager.AppSettings[RowSharePasswordConfigKey];

                // Not enough informations to connect
                if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(pwd))
                    return;

                var client = (HttpWebRequest)WebRequest.Create(RowShareUrl);
                client.Accept = "Application/json";
                client.ContentType = "Application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", login, pwd)));
                string[] responses = ((HttpWebResponse)client.GetResponse()).GetResponseHeader("Set-Cookie").Split(';');
                foreach (var resp in responses)
                {
                    if (resp.Contains(CookieName))
                    {
                        Cookie = resp.Substring(resp.IndexOf(CookieName) + CookieName.Length + 1);
                    }
                }
            }
        }

    }
}
