/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 战队restful API，
                 提供涉及到战队的服务。

******************************************************************************/

using HaiGame7.BLL;
using HaiGame7.Model.MyModel;
using HaiGame7.RestAPI.Filter;
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

        #region 我的默认战队
        /// <summary>
        /// 我的默认战队
        /// </summary>
        /// <param name="team">
        /// 参数实例：{Creater:"13439843883"}
        /// </param>
        /// <returns>
        /// 返回值实例：[{"MessageCode":0,"Message":null},
        /// {"Creater":64,"TeamName":"訾1","TeamLogo":"http://images.haigame7.com/logo/20160215144709XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"qqq","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":0,"WinCount":null,"LoseCount":null,"FollowCount":null,"Role":"teamcreater","CreateTime":"2016-10-01/"}]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage MyTeam([FromBody] SimpleTeamModel team)
        {
            TeamLogic teamLogic = new TeamLogic();
            jsonResult = teamLogic.MyTeam(team);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 创建战队
        /// <summary>
        /// 创建战队
        /// </summary>
        /// <param name="team">
        /// 参数实例：{Creater:"13439843883",TeamName:"氦7",TeamLogo:"图片url",TeamType:"DOTA2"}
        /// </param>
        /// <returns>
        /// 创建成功：{"MessageCode":0,"Message":""}
        /// 战队名称已存在：{"MessageCode":20001,"Message":"team exist"}
        /// 无权创建：{"MessageCode":20002,"Message":"you are teamuser"}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage Create([FromBody]  SimpleTeamModel team)
        {
            TeamLogic teamLogic = new TeamLogic();
            jsonResult = teamLogic.Create(team);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 更新战队信息

        #endregion

        #region 解散战队
        #endregion

        #region 获取战队列表
        /// <summary>
        /// 根据不同条件，获取战队列表
        /// </summary>
        /// <param name="para">
        /// type：1.createdate 注册日期 2.userfightscore 个人战斗力匹配 3.teamfightscore 战队战斗力匹配
        /// 参数实例：{"createUserID":111,"Type":"createdate","Sort":"desc","StartPage":1,"PageCount":10}
        /// </param>
        /// <returns>
        /// 返回值实例：[{"MessageCode":0,"Message":null},
        /// [{"Creater":92,"TeamName":"氦7通天塔","TeamLogo":"http://images.haigame7.com/logo/20160308160716XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"GTMA","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":0,"WinCount":0,"LoseCount":0,"FollowCount":1,"Role":null,"CreateTime":"2016-03-08"},
        /// {"Creater":65,"TeamName":"孟庆丰","TeamLogo":"http://images.haigame7.com/logo/20160308160601XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"123","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":1,"WinCount":0,"LoseCount":0,"FollowCount":0,"Role":null,"CreateTime":"2016-03-08"},
        /// {"Creater":65,"TeamName":"123","TeamLogo":"http://images.haigame7.com/logo/20160308160540XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"123","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":1,"WinCount":0,"LoseCount":0,"FollowCount":0,"Role":null,"CreateTime":"2016-03-08"},
        /// {"Creater":66,"TeamName":"平平一","TeamLogo":"http://images.haigame7.com/logo/20160229170559XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"栽植有东西","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":1,"WinCount":0,"LoseCount":0,"FollowCount":0,"Role":null,"CreateTime":"2016-02-29"},
        /// {"Creater":65,"TeamName":"bugbug","TeamLogo":"http://images.haigame7.com/logo/20160229170507XXKqu4W0Z5j3PxEIK0zW6uUR3LY=.png","TeamDescription":"DebugDebugDebugDebug","TeamType":"DOTA2","FightScore":0,"Asset":0,"IsDeault":1,"WinCount":0,"LoseCount":0,"FollowCount":0,"Role":null,"CreateTime":"2016-02-29"}]]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage TeamList([FromBody] TeamListParameterModel para)
        {
            TeamLogic teamLogic = new TeamLogic();
            jsonResult = teamLogic.TeamList(para);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

    }
}
