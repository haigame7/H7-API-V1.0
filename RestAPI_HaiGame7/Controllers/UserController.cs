/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 用户restful API，
                 提供涉及到用户的服务。

******************************************************************************/

using System.Net.Http;
using System.Text;
using System.Web.Http;
using HaiGame7.RestAPI.Filter;
using HaiGame7.BLL;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using HaiGame7.BLL.Filter;

namespace HaiGame7.RestAPI.Controllers
{
    /// <summary>
    /// 用户中心restful API，提供涉及到用户的服务。
    /// </summary>
    [AccessTokenFilter]
    [ExceptionFilter]
    public class UserController : ApiController
    {
        //初始化Response信息
        HttpResponseMessage returnResult = new HttpResponseMessage();
        //初始化返回结果
        string jsonResult;

        #region Login 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns>
        /// 登录成功：{"MessageCode":0,"Message":""}
        /// 手机号不存在：{"MessageCode":10001,"Message":"no user"}
        /// 密码错误：{"MessageCode":10002,"Message":"password error"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage Login([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.Login(user);

            returnResult.Content =new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region VerifyCode1 获取验证码（注册用）
        /// <summary>
        /// 获取验证码（注册用）
        /// </summary>
        /// <returns>
        /// 获取成功：{"MessageCode":0,"Message":"验证码"}
        /// 获取失败：{"MessageCode":10003,"Message":"verfitycode error"}
        /// 手机号已注册：{"MessageCode":10004,"Message":"user exist"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage VerifyCode1([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.VerifyCode1(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region VerifyCode2 获取验证码（找回密码用）
        /// <summary>
        /// 获取验证码（找回密码用）
        /// </summary>
        /// <returns>
        /// 获取成功：{"MessageCode":0,"Message":"验证码"}
        /// 手机号不存在：{"MessageCode":10001,"Message":"no user"}
        /// 验证码获取失败：{"MessageCode":10003,"Message":"verfitycode error"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage VerifyCode2([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.VerifyCode2(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region Register 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns>
        /// 注册成功：{"MessageCode":0,"Message":""}
        /// 手机号已存在：{"MessageCode":10004,"Message":"user exist"}
        /// 验证码错误：{"MessageCode":10005,"Message":"verifycode error"}
        /// 验证码过期：{"MessageCode":10006,"Message":"verifycode expire"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage Register([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.Register(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region ResetPassWord 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns>
        /// 重置成功：{"MessageCode":0,"Message":""}
        /// 手机号不存在：{"MessageCode":10001,"Message":"no user"}
        /// 验证码错误：{"MessageCode":10005,"Message":"verifycode error"}
        /// 验证码过期：{"MessageCode":10006,"Message":"verifycode expire"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage ResetPassWord([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.ResetPassWord(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 获取个人信息
        /// <summary>
        /// 根据手机号获取个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 返回值实例：[{"MessageCode":0,"Message":""},
        /// {"db_ArticleComment":[],"db_AssetRecord":[],"db_GameIDofUser":[],"db_GameRecord":[],"db_GuessRecord":[],"db_MyCollection":[],"db_Report":[],"db_Team":[],"UserID":64,"Openid":null,"NickName":null,"UserPicture":null,"UserName":null,"PhoneNumber":"13439843883","CardID":null,"UserPassWord":"5d354089d5b6378016dca832d3645dbf","UserWebNickName":"不服","UserWebPicture":"http://images.haigame7.com/avatar/20160127162940WxExqw0paJXAo1AtXc4RzGYo2LE=.png","StudentID":null,"GameID":"173032376","Picture":null,"IDCardPic1":null,"IDCardPic2":null,"EMail":null,"Address":"北京-大兴区","Sex":"男","Birthday":"\/Date(1458230400000)\/","RegisterDate":null,"Unit":null,"Job":null,"Hobby":null,"SysTime":[0,0,0,0,0,0,45,178]}]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage UserInfo([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.UserInfo(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 更改个人信息
        /// <summary>
        /// 更改个人信息，传入手机号和要更改的字段，不更改字段不要传
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 更改成功：{"MessageCode":0,"Message":""}
        /// 用户不存在：{"MessageCode":10001,"Message":"no user"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage UpdateUserInfo([FromBody] UserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.UpdateUserInfo(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 获取游戏数据

        #endregion

        #region 我的战斗力排名

        #endregion

        #region 认证游戏ID

        #endregion


        #region MyAssetList 我的资产列表
        /// <summary>
        /// 我的资产列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 返回值实例：[{"MessageCode":0,"Message":""},
        /// [{"db_User":null,"GainRecordID":145,"UserID":64,"TrueMoney":null,"VirtualMoney":-50,"GainTime":"\/Date(1456463195923)\/","State":"正常记录","GainWay":"接受挑战","Remark":"2016/2/26 13:06:35 接受挑战 消费虚拟币：-50","OutTradeno":null,"TransactionID":null,"TradeType":null,"TimeEnd":null,"SysTime":[0,0,0,0,0,0,45,158]},
        /// {"db_User":null,"GainRecordID":39,"UserID":64,"TrueMoney":null,"VirtualMoney":-50,"GainTime":"\/Date(1455513322253)\/","State":"正常记录","GainWay":"接受挑战","Remark":"2016/2/15 13:15:22 接受挑战 消费虚拟币：-50","OutTradeno":null,"TransactionID":null,"TradeType":null,"TimeEnd":null,"SysTime":[0,0,0,0,0,0,40,39]},
        /// {"db_User":null,"GainRecordID":38,"UserID":64,"TrueMoney":0.00,"VirtualMoney":-1,"GainTime":"\/Date(1455512812317)\/","State":"正常记录","GainWay":"参加竞猜","Remark":"2016/2/15 13:06:52 参加竞猜 消费虚拟币：-1","OutTradeno":null,"TransactionID":null,"TradeType":null,"TimeEnd":null,"SysTime":[0,0,0,0,0,0,40,37]},
        /// {"db_User":null,"GainRecordID":13,"UserID":64,"TrueMoney":0.00,"VirtualMoney":-1,"GainTime":"\/Date(1453949420567)\/","State":"正常记录","GainWay":"参加竞猜","Remark":"2016/1/28 10:50:20 参加竞猜 消费虚拟币：-1","OutTradeno":null,"TransactionID":null,"TradeType":null,"TimeEnd":null,"SysTime":[0,0,0,0,0,0,35,217]},
        /// {"db_User":null,"GainRecordID":2,"UserID":64,"TrueMoney":0.00,"VirtualMoney":1000,"GainTime":"\/Date(1453872526857)\/","State":"正常记录","GainWay":"注册","Remark":"2016/1/27 13:28:46 注册 获得虚拟币：50","OutTradeno":null,"TransactionID":null,"TradeType":null,"TimeEnd":null,"SysTime":[0,0,0,0,0,0,35,136]}]
        /// ]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage MyAssetList([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.MyAssetList(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 我的总资产
        /// <summary>
        /// 我的总资产，返回总氦金，和我的资产排名
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// 返回值实例：[{"MessageCode":0,"Message":""},{"TotalAsset":"898","MyRank":"8"}]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage MyTotalAsset([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.MyTotalAsset(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

    }
}