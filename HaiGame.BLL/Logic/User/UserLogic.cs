using System.Collections.Generic;
using System.Linq;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using HaiGame7.BLL.Enum;
using HaiGame7.BLL.Logic.Common;
using System.Web.Script.Serialization;
using System;

namespace HaiGame7.BLL
{
    public class UserLogic
    {
        #region 登录处理
        public string Login(SimpleUserModel user)
        {
            string result="";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser != null)
                {
                    if(dbUser.UserPassWord == user.PassWord)
                    {
                        //登录成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = Message.OK;
                    }
                    else
                    {
                        //密码错误
                        message.MessageCode = Message.PWSERR_CODE;
                        message.Message = Message.PWSERR;
                    }
                }
                else
                {
                    //用户未存在
                    message.MessageCode = Message.NOUSER_CODE;
                    message.Message = Message.NOUSER;
                }
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion

        #region 获取验证码
        public string VerifyCode(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser != null)
                {
                    //发送验证码
                    Dictionary<string,object> ret=SMSLogic.SendSMS(user.PhoneNumber,"");
                    //返回发送结果
                    if (ret["statusCode"].ToString()=="000000")
                    {
                        //获取验证码成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = Message.OK;
                    }
                    else
                    {
                        //获取验证码失败
                        message.MessageCode = Message.SMSERR_CODE;
                        message.Message = Message.SMSERR;
                    }
                }
                else
                {
                    //手机号不存在
                    message.MessageCode = Message.NOUSER_CODE;
                    message.Message = Message.NOUSER;
                }
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion

        #region 注册
        public string Register(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser != null)
                {
                    //发送验证码
                    Dictionary<string, object> ret = SMSLogic.SendSMS(user.PhoneNumber, "");
                    //返回发送结果
                    if (ret["statusCode"].ToString() == "000000")
                    {
                        //获取验证码成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = Message.OK;
                    }
                    else
                    {
                        //获取验证码失败
                        message.MessageCode = Message.SMSERR_CODE;
                        message.Message = Message.SMSERR;
                    }
                }
                else
                {
                    //手机号不存在
                    message.MessageCode = Message.NOUSER_CODE;
                    message.Message = Message.NOUSER;
                }
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion

        #region 重置密码
        public string ResetPassWord(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser != null)
                {
                    //发送验证码
                    Dictionary<string, object> ret = SMSLogic.SendSMS(user.PhoneNumber, "");
                    //返回发送结果
                    if (ret["statusCode"].ToString() == "000000")
                    {
                        //获取验证码成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = Message.OK;
                    }
                    else
                    {
                        //获取验证码失败
                        message.MessageCode = Message.SMSERR_CODE;
                        message.Message = Message.SMSERR;
                    }
                }
                else
                {
                    //手机号不存在
                    message.MessageCode = Message.NOUSER_CODE;
                    message.Message = Message.NOUSER;
                }
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion
    }
}
