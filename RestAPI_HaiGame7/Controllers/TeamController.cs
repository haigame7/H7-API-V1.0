/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 战队restful API，
                 提供涉及到战队的服务。

******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HaiGame7.RestAPI.Controllers
{    
    public class TeamController : ApiController
    {
        //初始化Response信息
        HttpResponseMessage returnResult = new HttpResponseMessage();

        public HttpResponseMessage Create()
        {
            return returnResult;
        }
    }
}
