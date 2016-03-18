/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 战队restful API，
                 提供涉及到战队的服务。

******************************************************************************/

using HaiGame7.BLL.Filter;
using HaiGame7.RestAPI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaiGame7.RestAPI.Controllers
{
    /// <summary>
    /// 战队中心restful API，提供涉及到用户的服务。
    /// </summary>
    [AccessTokenFilter]
    [ExceptionFilter]
    public class TeamController : ApiController
    {
        //初始化Response信息
        HttpResponseMessage returnResult = new HttpResponseMessage();
        //初始化返回结果
        string jsonResult;

        public HttpResponseMessage Create()
        {
            return returnResult;
        }
    }
}
