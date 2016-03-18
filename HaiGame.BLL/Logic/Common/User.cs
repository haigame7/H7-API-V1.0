using HaiGame7.Model.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.BLL.Logic.Common
{
    public class User
    {
        #region 通过手机号获取用户信息
        public static db_User GetUserByPhoneNumber(string phoneNumber)
        {
            db_User user;
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                user = context.db_User.Where(c => c.PhoneNumber == phoneNumber).FirstOrDefault();
            }
            return user;
        }
        #endregion

        #region 判断昵称是否存在
        public static bool GetUserByNickName(string nickName)
        {
            db_User user;
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                user = context.db_User.Where(c => c.UserWebNickName == nickName).FirstOrDefault();
            }
            if (user == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
