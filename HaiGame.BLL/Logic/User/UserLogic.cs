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
            HashSet<object> returnResult = new HashSet<object>();

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
                        message.MessageCode = MESSAGE.OK_CODE;
                        message.Message = MESSAGE.OK;
                    }
                    else
                    {
                        //密码错误
                        message.MessageCode = MESSAGE.PWSERR_CODE;
                        message.Message = MESSAGE.PWSERR;
                    }
                }
                else
                {
                    //用户未存在
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                    message.Message = MESSAGE.NOUSER;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取验证码（找回密码用）
        public string VerifyCode2(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

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
                        message.MessageCode = MESSAGE.OK_CODE;
                        message.Message = verifyCode;
                    }
                    else
                    {
                        //获取验证码失败
                        message.MessageCode = MESSAGE.SMSERR_CODE;
                        message.Message = MESSAGE.SMSERR;
                    }
                }
                else
                {
                    //手机号不存在
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                    message.Message = MESSAGE.NOUSER;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取验证码（注册用）
        public string VerifyCode1(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

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
                        message.MessageCode = MESSAGE.OK_CODE;
                        message.Message = verifyCode;
                    }
                    else
                    {
                        //获取验证码失败
                        message.MessageCode = MESSAGE.SMSERR_CODE;
                        message.Message = MESSAGE.SMSERR;
                    }
                }
                else
                {
                    //手机号已注册
                    message.MessageCode = MESSAGE.USEREXIST_CODE;
                    message.Message = MESSAGE.USEREXIST;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }    
        #endregion

        #region 注册
        public string Register(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser == null)
                {
                    db_AssetRecord assetRecord = new db_AssetRecord();
                    db_User userRecord = new db_User();

                    //添加信息到User表
                    userRecord.PhoneNumber = user.PhoneNumber;
                    MD5 md5Hash = MD5.Create();
                    userRecord.UserPassWord = Common.GetMd5Hash(md5Hash, user.PassWord);
                    userRecord.RegisterDate = DateTime.Now;
                    context.db_User.Add(userRecord);
                    context.SaveChanges();

                    //添加信息到资产表
                    db_User regUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                    Asset.AddMoneyRegister(regUser.UserID);
                    
                    //添加成功
                    message.MessageCode = MESSAGE.OK_CODE;
                    message.Message = MESSAGE.OK;
                }
                else
                {
                    //手机号已存在
                    message.MessageCode = MESSAGE.USEREXIST_CODE;
                    message.Message = MESSAGE.USEREXIST;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 重置密码
        public string ResetPassWord(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 判断手机号是否存在
                db_User dbUser = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber.Trim()).FirstOrDefault();
                if (dbUser == null)
                {
                    //手机号不存在
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                    message.Message = MESSAGE.NOUSER;
                }
                else
                {
                    //修改密码
                    MD5 md5Hash = MD5.Create();
                    dbUser.UserPassWord= Common.GetMd5Hash(md5Hash, user.PassWord);
                    context.SaveChanges();
                    //修改成功
                    message.MessageCode = MESSAGE.OK_CODE;
                    message.Message = MESSAGE.OK;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 根据手机号获取我的个人信息
        public string UserInfo(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                UserModel userInfo = User.GetUserModelByPhoneNumber(user.PhoneNumber);
                if (userInfo!=null)
                {
                    message.Message = MESSAGE.OK;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
                returnResult.Add(message);
                returnResult.Add(userInfo);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 根据昵称获取个人信息
        public string UserInfoByNickName(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                UserModel userInfo = User.GetUserModelByNickName(user.PhoneNumber);
                if (userInfo != null)
                {
                    message.Message = MESSAGE.OK;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
                returnResult.Add(message);
                returnResult.Add(userInfo);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 更改个人信息
        public string UpdateUserInfo(UserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                db_User userInfo = context.db_User.Where(c => c.PhoneNumber == user.PhoneNumber).FirstOrDefault();

                if (userInfo != null)
                {
                    int code = MESSAGE.OK_CODE;
                    string msg = MESSAGE.OK;

                    #region 个人信息字段
                    if (user.Address!=null)
                    {
                        userInfo.Address = user.Address;
                    }
                    if (user.Birthday != null)
                    {
                        userInfo.Birthday = DateTime.Parse(user.Birthday);
                    }
                    if (user.Hobby != null)
                    {
                        userInfo.Hobby = user.Hobby;
                    }
                    if (user.Sex != null)
                    {
                        userInfo.Sex = user.Sex;
                    }
                    if (user.UserWebNickName != null)
                    {
                        //验证昵称是否存在
                        if(User.GetUserByNickName(user.UserWebNickName.Trim()))
                        {
                            userInfo.UserWebNickName = user.UserWebNickName;
                        }
                        else
                        {
                            //昵称已存在
                            msg = MESSAGE.NICKEXIST;
                            code = MESSAGE.NICKEXIST_CODE;  
                        }
                    }
                    if (user.UserWebPicture != null)
                    {
                        userInfo.UserWebPicture = Common.Base64ToImage(user.UserWebPicture);
                    }
                    #endregion
                    context.SaveChanges();
                    message.Message = msg;
                    message.MessageCode = code;
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取我的资产列表
        public string MyAssetList(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            //获取我的资产
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                db_User userInfo = User.GetUserByPhoneNumber(user.PhoneNumber);
                //获取用户资产列表
                // 获取用户游戏数据
                var sql = "SELECT t1.VirtualMoney,CONVERT(varchar(100), t1.GainTime, 23) as GainTime,t1.GainWay,t2.Remark" +
                          " FROM db_AssetRecord t1" +
                          " WHERE t1.UserID = " + userInfo.UserID + "ORDER BY t1.GainTime DESC";

                var assetList = context.Database.SqlQuery<AssetList>(sql)
                                 .ToList();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                returnResult.Add(message);
                returnResult.Add(assetList);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取我的总资产
        public string MyTotalAsset(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            MyAssetModel myAsset = new MyAssetModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            //获取我的资产
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                db_User userInfo = User.GetUserByPhoneNumber(user.PhoneNumber);
                if (userInfo!=null)
                {
                    //获取用户总资产
                    var asset = context.db_AssetRecord.Where(c => c.UserID == userInfo.UserID).Sum(c => c.VirtualMoney);
                    myAsset.TotalAsset = (int)asset;
                    //获取用户资产排名
                    myAsset.MyRank = Asset.MyRank(myAsset.TotalAsset, (DateTime)userInfo.RegisterDate);

                    message.Message = MESSAGE.OK;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
                
                returnResult.Add(message);
                returnResult.Add(myAsset);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我的游戏数据
        public string MyGameInfo(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            GameModel gameInfo=new GameModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {

                //获取用户
                db_User userInfo = User.GetUserByPhoneNumber(user.PhoneNumber);
                if (userInfo != null)
                {
                    // 获取用户游戏数据
                    var sql = "select t1.UserID,t1.GameID,t1.CertifyState,t2.GamePower,t1.CertifyName" +
                            " from db_GameIDofUser t1" +
                            " left join db_GameInfoofPlatform t2" +
                            " on t1.UGID = t2.UGID" +
                            " where t1.UserID = " + userInfo.UserID + " and t1.GameType = 'DOTA2'";

                    gameInfo = context.Database.SqlQuery<GameModel>(sql)
                                     .FirstOrDefault();

                    if (gameInfo == null)
                    {
                        //无游戏数据
                        message.Message = MESSAGE.NOGAMEDATA;
                        message.MessageCode = MESSAGE.NOGAMEDATA_CODE;
                    }
                    else
                    {
                        message.Message = MESSAGE.OK;
                        message.MessageCode = MESSAGE.OK_CODE;
                    }
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }

                returnResult.Add(message);
                returnResult.Add(gameInfo);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 提交认证游戏ID
        public string CertifyGameID(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                db_User userInfo = User.GetUserByPhoneNumber(user.PhoneNumber);
                if (userInfo != null)
                {
                    db_GameIDofUser gameIDofUser=new db_GameIDofUser();
                    gameIDofUser.UserID = userInfo.UserID;   
                    gameIDofUser.GameID = user.GameID;
                    gameIDofUser.GameType = "DOTA2";
                    gameIDofUser.CertifyState = 0;
                    gameIDofUser.CertifyName = "氦七"+Common.MathRandom(6);
                    context.db_GameIDofUser.Add(gameIDofUser);
                    context.SaveChanges();

                    message.Message = gameIDofUser.CertifyName;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                else
                {
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 更改认证游戏ID
        public string UpdateCertifyGameID(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //获取用户
                db_User userInfo = User.GetUserByPhoneNumber(user.PhoneNumber);
                if (userInfo != null)
                {
                    db_GameIDofUser gameIDofUser =context.db_GameIDofUser.
                                    Where(c=>c.UserID== userInfo.UserID).
                                    Where(c => c.GameType == "DOTA2").
                                    FirstOrDefault();

                    gameIDofUser.GameID = user.GameID;
                    gameIDofUser.CertifyState = 0;
                    gameIDofUser.CertifyName = "氦七" + Common.MathRandom(6);
                    context.SaveChanges();
                    //返回认证昵称
                    message.Message = gameIDofUser.CertifyName;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                else
                {
                    //无用户信息
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
