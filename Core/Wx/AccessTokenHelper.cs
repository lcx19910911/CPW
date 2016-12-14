using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
namespace Core
{
    /// <summary>
    /// 把access_token存到cookie里面 
    /// </summary>
    public class AccessTokenHelper
    {

        /// <summary>
        /// 得到access_token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            //判断当前cookie是否存在
            HttpCookie accessTokenCookie = HttpContext.Current.Request.Cookies["access_token"];
            if (accessTokenCookie == null||string.IsNullOrEmpty(accessTokenCookie.Value))
            {
                AccessTokenJson token = GetWeChatToken();
                if (token != null)
                {
                    accessTokenCookie = new HttpCookie("access_token", token.access_token);
                    accessTokenCookie.Expires = DateTime.Now.AddSeconds(token.expires_in);
                    HttpContext.Current.Response.Cookies.Set(accessTokenCookie);
                    return token.access_token;
                }
                else
                    return "";
            }
            else
            {
                return accessTokenCookie.Value;
            }
        }

        /// <summary>
        /// 请求微信服务器得到access_token
        /// </summary>
        /// <returns></returns>
        private static AccessTokenJson GetWeChatToken()
        {
            try
            {
                string appId = ConfigurationManager.AppSettings["appid"];
                string secret = ConfigurationManager.AppSettings["appsecret"];
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appId + "&secret=" + secret + "";
                string result = GetRespone(url);
                AccessTokenJson json = result.DeserializeJson<AccessTokenJson>();
                if (json == null)
                {
                    return null;
                }
                else
                    return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 得到网站接口的返回数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRespone(string url)
        {
            System.Net.HttpWebRequest HttpPost = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            HttpPost.Timeout = 5000;
            HttpPost.Method = "get";
            HttpPost.ContentType = "application/x-www-form-urlencoded";
            HttpPost.AllowAutoRedirect = true;

            System.Net.HttpWebResponse HttpResponse = (System.Net.HttpWebResponse)HttpPost.GetResponse();
            System.IO.StreamReader ReadStream = new System.IO.StreamReader(HttpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("gbk"));
            string getStr = ReadStream.ReadToEnd();
            return getStr;
        }

    }

    public class AccessTokenJson
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }
}