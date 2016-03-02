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
        /// 获取成功：{"MessageCode":0,"Message":""}
        /// 手机号不存在：{"MessageCode":10001,"Message":"no user"}
        /// 验证码获取失败：{"MessageCode":10003,"Message":"verfitycode error"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage VerifyCode1([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.VerifyCode(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region VerifyCode2 获取验证码（找回密码用）
        /// <summary>
        /// 获取验证码（找回密码用）
        /// </summary>
        /// <returns>
        /// 获取成功：{"MessageCode":0,"Message":""}
        /// 手机号不存在：{"MessageCode":10001,"Message":"no user"}
        /// 验证码获取失败：{"MessageCode":10003,"Message":"verfitycode error"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage VerifyCode2([FromBody] SimpleUserModel user)
        {
            UserLogic userLogic = new UserLogic();
            jsonResult = userLogic.VerifyCode(user);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region Register 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns>
        /// 
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
        /// 
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
    }
}