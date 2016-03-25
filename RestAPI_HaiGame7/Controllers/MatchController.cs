/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 赛事restful API，
                 提供涉及到赛事的服务。

******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaiGame7.RestAPI.Controllers
{
    /// <summary>
    /// 赛事restful API，提供涉及到赛事的服务。
    /// </summary>
    public class MatchController : ApiController
    {
        //初始化Response信息
        HttpResponseMessage returnResult = new HttpResponseMessage();
        //初始化返回结果
        string jsonResult;

        [HttpPost]
        public HttpResponseMessage MatchList()
        {
            MatchLogic fightLogic = new MatchLogic();
            jsonResult = fightLogic.MatchList();

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
    }
}
