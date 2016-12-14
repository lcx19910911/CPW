using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class RegularHelper
    {
        public const string ValiteEmail = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

        public const string EmailErrorMsg = "请输入正确邮箱地址";

        public const string ValitePhone = @"^((d{3,4})|d{3,4}-)?d{7,8}$";
        public const string PhoneErrorMsg = "请输入正确的座机号码";

        public const string ValiteMobile = @"^1[0-9]{10}$";
        public const string MobileErrorMsg = "请输入正确的手机号码";

        public const string ValiteUrl = @"^(http|https)://([w-]+.)+[w-]+(/[w-./?%&=]*)?$";
        public const string UrlErrorMsg = "请输入正确链接";

        public const string ValiteOnlyNumberAndletter = @"^[A-Za-z0-9]+$";
        public const string OnlyNumberAndletterErrorMsg = "只能包含数字和英文字符";

        public const string ValiteIDCard15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        public const string IDCard15ErrorMsg = "请输入15位正确的身份证号码";

        public const string ValiteIDCard18 = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$";
        public const string IDCard18ErrorMsg = "请输入18位正确的身份证号码";
    }
}
