using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
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

        #region 通过手机号获取用户信息
        public static UserModel GetUserModelByPhoneNumber(string phoneNumber)
        {
            UserModel user;
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var sql = "SELECT t1.UserID,t1.PhoneNumber,t1.UserWebNickName," +
                         "  t1.UserWebPicture,t1.UserName,t1.Address,t1.Sex,CONVERT(varchar(100), t1.Birthday, 23) as Birthday,t1.Hobby" +
                         "  FROM db_User t1 WHERE t1.PhoneNumber= "+phoneNumber+"";

                user = context.Database.SqlQuery<UserModel>(sql)
                                 .FirstOrDefault();
            }
            return user;
        }
        #endregion

        #region 通过昵称获取用户信息
        public static UserModel GetUserModelByNickName(string nickName)
        {
            UserModel user;
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var sql = "SELECT t1.UserID,t1.PhoneNumber,t1.UserWebNickName," +
                         "  t1.UserWebPicture,t1.UserName,t1.Address,t1.Sex,CONVERT(varchar(100), t1.Birthday, 23) as Birthday,t1.Hobby" +
                         "  FROM db_User t1 WHERE t1.UserWebNickName= " + nickName + "";

                user = context.Database.SqlQuery<UserModel>(sql)
                                 .FirstOrDefault();
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
