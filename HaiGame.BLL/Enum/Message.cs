﻿using System;
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
        public const int NOUSER_CODE = 10001;
        public const string NOUSER = "no user";

        //密码错误
        public const int PWSERR_CODE = 10002;
        public const string PWSERR = "password error";

        //验证码获取失败
        public const int SMSERR_CODE = 10003;
        public const string SMSERR = "verfitycode error";

        //手机号已注册
        public const int USEREXIST_CODE = 10004;
        public const string USEREXIST = "user exist";

        //验证码错误
        public const int VERIFYERR_CODE = 10005;
        public const string VERIFYERR = "verifycode error";

        //验证码过期
        public const int VERIFYEXPIRE_CODE = 10006;
        public const string VERIFYEXPIRE = "verifycode expire";

        //系统错误
        public const int SYSERR_CODE = 40001;
        public const string SYSERR = "system error";

        //没有AccessToken
        public const int NOACCESSTOKEN_CODE = 40002;
        public const string NOACCESSTOKEN = "no accesstoken";

        //AccessToken 无效
        public const int ACCESSTOKENINVALID_CODE = 40003;
        public const string ACCESSTOKENINVALID = "invalid accesstoken";
    }
}
