
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core
{
    public static class CookieHelper
    {

        //更新用户的cookie
        public static void CreateCookie(User user)
        {
            try
            {
                var obj = CryptoHelper.AES_Encrypt(user.ToJson(), Params.SecretKey);
                HttpContext.Current.Session[Params.UserCookieName] = obj;
            }
            catch (Exception ex){
                LogHelper.WriteException(ex);
            }
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            var  obj = HttpContext.Current.Session[Params.UserCookieName];
            if (obj == null)
                return null;
            
            User user = (CryptoHelper.AES_Decrypt(obj.ToString(), Params.SecretKey)).DeserializeJson<User>();
            return user;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static void ClearCookie()
        {
            HttpContext.Current.Session[Params.UserCookieName] = "";
        }

    }
}
