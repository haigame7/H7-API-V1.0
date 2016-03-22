/******************************************************************************

** author:zihai

** create date:2016-02-18

** update date:2016-02-18

** description : 约战restful API，
                 提供涉及到约战的服务。

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
    /// 约战restful API，提供涉及到约战的服务。
    /// </summary>
    public class FightController : ApiController
    {
        //初始化Response信息
        HttpResponseMessage returnResult = new HttpResponseMessage();
        //初始化返回结果
        string jsonResult;

        #region 约战动态列表
        /// <summary>
        /// 约战动态列表
        /// </summary>
        /// <param name="rank">
        /// 参数说明：
        /// startpage：页码
        /// pagecount：每页记录数
        /// 参数实例：{"startpage":1,"pagecount":5}
        /// </param>
        /// <returns>
        /// [{"MessageCode":0,"Message":""},
        /// [{"Description":"战队【高1】接受战队【ddddd】的约战","FightTime":"2月22日","FightAsset":50},
        /// {"Description":"战队【潜水泵战队】接受战队【国睡战队】的约战","FightTime":"2月24日","FightAsset":50},
        /// {"Description":"战队【奶粉哪去了】接受战队【潜水泵战队】的约战","FightTime":"2月24日","FightAsset":1},
        /// {"Description":"战队【aaaaaaaaaa】接受战队【国睡战队】的约战","FightTime":"2月24日","FightAsset":50},
        /// {"Description":"战队【通天塔战队】接受战队【ddddd】的约战","FightTime":"2月25日","FightAsset":50},
        /// {"Description":"战队【奶粉哪去了】向战队【通天塔战队】认怂","FightTime":"2月25日","FightAsset":50},
        /// {"Description":"战队【通天塔战队】接受战队【通地塔】的约战","FightTime":"2月26日","FightAsset":50},
        /// {"Description":"战队【huhaoran】向战队【通地塔】认怂","FightTime":"2月26日","FightAsset":2000},
        /// {"Description":"战队【huhaoran】向战队【通地塔】认怂","FightTime":"2月26日","FightAsset":2000},
        /// {"Description":"战队【訾1】接受战队【通天塔战队】的约战","FightTime":"2月26日","FightAsset":50}]]
        /// </returns>
        [HttpPost]
        public HttpResponseMessage AllFightList([FromBody] RankParameterModel rank)
        {
            FightLogic fightLogic = new FightLogic();
            jsonResult = fightLogic.AllFightList(rank);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion

        #region 发起约战
        /// <summary>
        /// 发起约战
        /// </summary>
        /// <param name="para"></param>
        /// <returns>
        /// 约战成功：{"MessageCode":0,"Message":""}
        /// </returns>
        [HttpPost]
        public HttpResponseMessage MakeChallenge([FromBody] ChallengeParameterModel para)
        {
            FightLogic fightLogic = new FightLogic();
            jsonResult = fightLogic.MakeChallenge(para);

            returnResult.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return returnResult;
        }
        #endregion
    }
}
