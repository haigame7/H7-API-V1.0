﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaiGame7.BLL.Enum;
using System.Security.Cryptography;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;

namespace HaiGame7.BLL.Logic.Common
{
    public class Common
    {
        #region 发送短信验证码
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="randomNum"></param>
        /// <returns></returns>
        public static Dictionary<string, object> SendSMS(string phoneNumber, string randomNum)
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            string[] data = { randomNum, SMS.TIME_OUT };
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init(SMS.ADDRESS, SMS.PORT);
            api.setAccount(SMS.ACCOUNT_SID, SMS.ACCOUNT_TOKEN);
            api.setAppId(SMS.APP_ID);
            if (isInit)
            {
                retData = api.SendTemplateSMS(phoneNumber, SMS.TEMPLATE_ID, data);
            }
            return retData;
        }
        #endregion

        #region 验证accesstoken是否合法
        /// <summary>
        /// 验证accesstoken是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsValid(string token)
        {
            if (token == "ABC12abc")
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 产生随机验证码
        /// <summary>
        /// 随机验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MathRandom(int? length)
        {
            string a = "0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
            }
            return sb.ToString();
        }
        #endregion

        #region MD5 加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

    }
}
