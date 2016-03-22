using HaiGame7.BLL.Enum;
using HaiGame7.BLL.Logic.Common;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HaiGame7.BLL
{
    public class RankLogic
    {
        #region 个人排行
        public string UserRank(RankParameterModel rank)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            //个人排行：昵称，签名，氦金，战斗力，大神系数
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //联合查询
                var sql = "select" +
                          " t1.UserWebNickName as NickName," +
                          " t1.UserWebPicture as UserPicture,"+
                          " t1.Hobby," +
                          " t2.GameID," +
                          " t3.GamePower," +
                          " t3.GameGrade," +
                          " (select sum(t4.VirtualMoney) from" +
                          " db_AssetRecord t4 where t4.UserID = t1.UserID) as Asset" +
                          " from db_User t1 left join" +
                          " db_GameIDofUser t2 on t1.UserID = t2.UserID" +
                          " left join db_GameInfoofPlatform t3" +
                          " on t2.UGID = t3.UGID" +
                          " where t2.GameType = 'DOTA2'"+
                          " order by "+ rank.RankType+ " "+rank.RankSort+",t1.RegisterDate ";

                 var  userRank = context.Database.SqlQuery<UserRankModel>(sql)
                                  .Skip((rank.StartPage - 1) * rank.PageCount)
                                  .Take(rank.PageCount).ToList();

                if (userRank == null)
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
                returnResult.Add(message);
                returnResult.Add(userRank);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 战队排行
        public string TeamRank(RankParameterModel rank)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            //团队排行：战队名称，战斗力，氦金，热度
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var sql = "SELECT" +
                          " t.TeamName," +
                          " t.TeamDescription," +
                          " t.TeamPicture," +
                          " (CASE WHEN t.FightScore IS NULL THEN 0 ELSE t.FightScore END) as FightScore," +
                          " (CASE WHEN t.Asset IS NULL THEN 0 ELSE t.Asset END) as Asset," +
                          " (" +
                          " ((CASE WHEN t.WinCount IS NULL THEN 0 ELSE t.WinCount END)" +
                          " +(CASE WHEN t.LoseCount IS NULL THEN 0 ELSE t.LoseCount END)" +
                          " +(CASE WHEN t.FollowCount IS NULL THEN 0 ELSE t.FollowCount END))*10" +
                          " + (CASE WHEN t.WinCount IS NULL THEN 0 ELSE t.WinCount END)*10 +" +
                          " (CASE WHEN t.FightScore IS NULL THEN 0 ELSE t.FightScore END)+" +
                          " (CASE WHEN t.Asset IS NULL THEN 0 ELSE t.Asset END))/ 3 as HotScore" +
                          " FROM db_Team t" +
                          " WHERE t.State = 0" +
                          " ORDER BY " + rank.RankType + " " + rank.RankSort + ", t.SysTime";

                 var teamRank = context.Database.SqlQuery<TeamRankModel>(sql)
                                 .Skip((rank.StartPage - 1) * rank.PageCount)
                                 .Take(rank.PageCount).ToList();

                if (teamRank == null)
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
                returnResult.Add(message);
                returnResult.Add(teamRank);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
