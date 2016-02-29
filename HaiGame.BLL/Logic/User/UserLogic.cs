using System.Collections.Generic;
using System.Linq;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using HaiGame7.BLL.Enum;
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
    }
}
