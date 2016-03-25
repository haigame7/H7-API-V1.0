﻿/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 赛事restful API，
                 提供涉及到赛事的服务。

******************************************************************************/

using HaiGame7.BLL;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        #region 赛事列表
        /// <summary>
        /// 赛事列表
        /// </summary>
        /// <returns>
        /// 返回实例：[{"MessageCode":0,"Message":""},
        /// [{"MatchID":4,"MatchName":"test","ShowPicture":"test","Introduce":"test"}]]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage MatchList()
        {
            MatchLogic matchLogic = new MatchLogic();
            jsonResult = matchLogic.MatchList();

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 主播列表
        /// <summary>
        /// 主播列表
        /// </summary>
        /// <param name="match">
        /// 传入参数：{MatchID:"4"}
        /// </param>
        /// <returns>
        /// 返回值实例：
        /// [{"MessageCode":0,"Message":""},
        /// [{"MatchID":4,"Count":20,"TalkShow":"加入我吧","Name":"MM","UserPicture":"http://images.haigame7.com/avatar/20160128135746WxExqw0paJXAo1AtXc4RzGYo2LE=.png","Sex":"女","Introduce":"ZZZZZZZZZZZZZZZ","Age":null,"GameID":"12345"}]]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage BoBoList([FromBody] MatchModel match)
        {
            MatchLogic matchLogic = new MatchLogic();
            jsonResult = matchLogic.BoBoList(match);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 已报名战队数
        /// <summary>
        /// 主播已报名战队数
        /// </summary>
        /// <param name="match">
        /// 参数实例：{matchid:"4",boboid:"4"}
        /// </param>
        /// <returns>
        ///返回实例值：[{"MessageCode":0,"Message":""},{"JoinCount":0}]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage BoBoCount([FromBody] MatchParameterModel match)
        {
            MatchLogic matchLogic = new MatchLogic();
            jsonResult = matchLogic.BoBoCount(match);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 报名参赛
        /// <summary>
        /// 报名参赛
        /// </summary>
        /// <param name="match">
        /// 参数实例：{matchid:"4",boboid:"4",TeamID:"11",PhoneNumber:"13439843883"}
        /// </param>
        /// <returns>
        /// 返回实例：[{"MessageCode":0,"Message":""}]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage JoinMatch([FromBody] MatchParameter2Model match)
        {
            MatchLogic matchLogic = new MatchLogic();
            jsonResult = matchLogic.JoinMatch(match);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 我的已报名信息
        /// <summary>
        /// 只传两个参数，参数实例：{matchid:"4",TeamID:"11"}
        /// </summary>
        /// <param name="match">
        /// [{"MessageCode":0,"Message":""}，{Name:"MM",ApplyTime:"2015-01-01 11:11:11"}]
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage MyJoinMatch([FromBody] MatchParameter2Model match)
        {
            MatchLogic matchLogic = new MatchLogic();
            jsonResult = matchLogic.MyJoinMatch(match);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion
    }
}
