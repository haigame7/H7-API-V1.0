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
        /// 用户登录处理
        /// </summary>
        /// <returns>
        /// 登录成功：{"MessageCode":0,"Message":""}
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

        #region VerifyCode 获取验证码
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        public HttpResponseMessage VerifyCode([FromBody] db_User user)
        {

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 注册 Register
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        public HttpResponseMessage Register([FromBody] db_User user)
        {

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion
    }
}
