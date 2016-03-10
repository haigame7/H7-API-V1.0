using System.Collections.Generic;
using System.Linq;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using HaiGame7.BLL.Enum;
using HaiGame7.BLL.Logic.Common;
using System.Web.Script.Serialization;
using System;
using System.Security.Cryptography;

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
                    MD5 md5Hash = MD5.Create();
                    if (dbUser.UserPassWord == Common.GetMd5Hash(md5Hash,user.PassWord))
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

        #region 获取验证码（找回密码用）
        public string VerifyCode2(SimpleUserModel user)
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
                    //验证码
                    string verifyCode = Common.MathRandom(4);
                    //发送验证码
                    Dictionary<string,object> ret=Common.SendSMS(user.PhoneNumber, verifyCode);
                    //返回发送结果
                    if (ret["statusCode"].ToString()=="000000")
                    {
                        //获取验证码成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = verifyCode;
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

        #region 获取验证码（注册用）
        public string VerifyCode1(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser == null)
                {
                    //验证码
                    string verifyCode = Common.MathRandom(4);
                    //发送验证码
                    Dictionary<string, object> ret = Common.SendSMS(user.PhoneNumber, verifyCode);
                    //返回发送结果
                    if (ret["statusCode"].ToString() == "000000")
                    {
                        //获取验证码成功
                        message.MessageCode = Message.OK_CODE;
                        message.Message = verifyCode;
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
                    //手机号已注册
                    message.MessageCode = Message.USEREXIST_CODE;
                    message.Message = Message.USEREXIST;
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
                    db_AssetRecord assetRecord = new db_AssetRecord();
                    db_User userRecord = new db_User();

                    //添加信息到User表
                    userRecord.PhoneNumber = user.PhoneNumber;
                    
                    MD5 md5Hash = MD5.Create();
                    userRecord.UserPassWord = Common.GetMd5Hash(md5Hash, user.PassWord);
                    
                    context.db_User.Add(userRecord);
                    //添加信息到资产表
                    //context.db_AssetRecord.Add();
                    context.SaveChanges();
                    //添加成功
                    message.MessageCode = Message.OK_CODE;
                    message.Message = Message.OK;
                }
                else
                {
                    //手机号已存在
                    message.MessageCode = Message.USEREXIST_CODE;
                    message.Message = Message.USEREXIST;
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
                    //手机号不存在
                    message.MessageCode = Message.NOUSER_CODE;
                    message.Message = Message.NOUSER;
                }
                else
                {
                    //修改密码
                    MD5 md5Hash = MD5.Create();
                    dbUser.UserPassWord= Common.GetMd5Hash(md5Hash, user.PassWord);
                    context.SaveChanges();
                    //修改成功
                    message.MessageCode = Message.OK_CODE;
                    message.Message = Message.OK;
                }
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion
    }
}
