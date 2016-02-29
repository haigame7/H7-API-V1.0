using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.BLL.Enum
{
    public static class Message
    {
        //执行成功
        public const int OK_CODE = 0;
        public const string OK = "";

        //没有用户
        public const int NOUSER_CODE = 100001;
        public const string NOUSER = "no user";

        //密码错误
        public const int PWSERR_CODE = 100002;
        public const string PWSERR = "password error";

        //系统错误
        public const int SYSERR_CODE = 400001;
        public const string SYSERR = "system error";

        //没有AccessToken
        public const int NOACCESSTOKEN_CODE = 400002;
        public const string NOACCESSTOKEN = "no accesstoken";

        //AccessToken 无效
        public const int ACCESSTOKENINVALID_CODE = 400003;
        public const string ACCESSTOKENINVALID = "invalid accesstoken";
    }
}
