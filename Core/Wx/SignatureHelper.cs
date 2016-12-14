using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Core
{
    public static class SignatureHelper
    {
        /// 获取JS-SDK权限验证的签名
        /// </summary>
        /// <returns></returns>
        public static ReturnStr GetSignature(string timestamp, string nonceStr)
        {
            try
            {
                //这里是获取jsapi_ticket  方法就不粘贴了
                ReturnStr ob = JsApiTicketHelper.GetJsApiTicket(timestamp, nonceStr);
                if (ob == null)
                    return null;
                string url = HttpContext.Current.Request.Url.ToString();
                url = url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url;
                //对所有待签名参数按照字段名的ASCII 码从小到大排序
                var string1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ob.jsApiTicket, ob.nonceStr, ob.timestamp, url);
                // 对string1进行sha1签名，得到signature
                var signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1");
                ob.signature=signature.ToLower().Replace("-","");
                return ob;
                //return signature.ToUpper();
            }
            catch
            {
                return null;
            }
        }
    }
}