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
    public class FightLogic
    {
        #region 约战动态列表
        public string AllFightList(RankParameterModel rank)
        {
            string result = "";
            MessageModel message = new MessageModel();
            List<FightStateModel> fightStateList = new List<FightStateModel>();
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            //获取约战平台约战记录的当前状态
            //个人排行：昵称，签名，氦金，战斗力，大神系数
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //联合查询
                var sql = "SELECT" +
                           " t1.DateID,t1.STeamID,t1.ETeamID,t1.Money,t1.FightAddress,t1.CurrentState," +
                           " t2.StateTime,t3.TeamName as STeamName,t4.TeamName as ETeamName" +
                           " FROM" +
                           " db_DateFight t1 join db_FightState t2" +
                           " ON t1.DateID = t2.DateID and t1.CurrentState = t2.State" +
                           " LEFT JOIN db_Team t3" +
                           " ON t1.STeamID = t3.TeamID" +
                           " LEFT JOIN db_Team t4" +
                           " ON t1.ETeamID = t4.TeamID ORDER BY t2.StateTime DESC";

                var fightStateDetailList = context.Database.SqlQuery<FightStateDetailModel>(sql)
                                 .Skip((rank.StartPage - 1) * rank.PageCount)
                                 .Take(rank.PageCount).ToList();

                if (fightStateDetailList == null)
                {
                    //无游戏数据
                    message.Message = MESSAGE.NOGAMEDATA;
                    message.MessageCode = MESSAGE.NOGAMEDATA_CODE;
                }
                else
                {
                    foreach (FightStateDetailModel fight in fightStateDetailList)
                    {
                        //拼接返回字段信息
                        FightStateModel fightState = new FightStateModel();
                        fightState.FightAsset = fight.Money;
                        fightState.FightTime = Common.DateDiff(fight.StateTime, DateTime.Now);
                        fightState.Description = Fight.FightState(fight);
                        fightStateList.Add(fightState);
                    }
                    message.Message = MESSAGE.OK;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
                returnResult.Add(message);
                returnResult.Add(fightStateList);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 发起约战
        public string MakeChallenge(ChallengeParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            List<FightStateModel> fightStateList = new List<FightStateModel>();
            HashSet<object> returnResult = new HashSet<object>();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;

               

                if (message.MessageCode==0)
                {
                    //向约战记录表插入一条数据
                    //向约战状态表插入一条数据
                    //向信息表插入一条数据
                }
            }
                
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我发出的约战
        public string MyFight(FightParameterModel fight)
        {
            if (fight.FightType.ToLower() == "send")
            {
                return MySendFight(fight);
            }
            else
            {
                return MyReceiveFight(fight);
            }
        }
        #endregion

        #region 我发出的约战
        public static string MySendFight(FightParameterModel fight)
        {
            string result = "";
            MessageModel message = new MessageModel();
            List<FightStateModel> fightStateList = new List<FightStateModel>();
            HashSet<object> returnResult = new HashSet<object>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<FightStateDetailModel> fightSendList;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                string teamID = "()";
                db_User userInfo = context.db_User.Where(c => c.PhoneNumber == fight.PhoneNumber).FirstOrDefault();
                if (userInfo != null)
                {
                    teamID=Team.MyAllTeamID(userInfo.UserID);
                }
                var sql = "SELECT" +
                          " t1.DateID,t1.STeamID,t1.ETeamID,t1.Money,t1.FightAddress,t1.CurrentState," +
                          " CONVERT(varchar(100), t2.StateTime, 20) as StateTimeStr," +
                          "　t3.TeamName as STeamName,t4.TeamName as ETeamName" +
                          " FROM" +
                          " db_DateFight t1 join db_FightState t2" +
                          " ON t1.DateID = t2.DateID and t1.CurrentState = t2.State" +
                          " LEFT JOIN db_Team t3" +
                          " ON t1.STeamID = t3.TeamID" +
                          " LEFT JOIN db_Team t4" +
                          " ON t1.ETeamID = t4.TeamID" +
                          " WHERE t3.State=0 AND t4.State=0 AND t1.STeamID IN" + teamID +
                          " ORDER BY t2.StateTime DESC";

                fightSendList = context.Database.SqlQuery<FightStateDetailModel>(sql)
                                 .Skip((fight.StartPage - 1) * fight.PageCount)
                                 .Take(fight.PageCount).ToList();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
            }

            returnResult.Add(message);
            returnResult.Add(fightSendList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我收到的约战
        public static string MyReceiveFight(FightParameterModel fight)
        {
            string result = "";
            MessageModel message = new MessageModel();
            List<FightStateModel> fightStateList = new List<FightStateModel>();
            HashSet<object> returnResult = new HashSet<object>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<FightStateDetailModel> fightReceiveList;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                string teamID = "()";
                db_User userInfo = context.db_User.Where(c => c.PhoneNumber == fight.PhoneNumber).FirstOrDefault();
                if (userInfo != null)
                {
                    teamID=Team.MyAllTeamID(userInfo.UserID);
                }
                var sql = "SELECT" +
                          " t1.DateID,t1.STeamID,t1.ETeamID,t1.Money,t1.FightAddress,t1.CurrentState," +
                          " CONVERT(varchar(100), t2.StateTime, 20) as StateTimeStr," +
                          " t3.TeamName as STeamName,t4.TeamName as ETeamName" +
                          " FROM" +
                          " db_DateFight t1 join db_FightState t2" +
                          " ON t1.DateID = t2.DateID and t1.CurrentState = t2.State" +
                          " LEFT JOIN db_Team t3" +
                          " ON t1.STeamID = t3.TeamID" +
                          " LEFT JOIN db_Team t4" +
                          " ON t1.ETeamID = t4.TeamID" +
                          " WHERE t3.State=0 AND t4.State=0 AND t1.ETeamID IN" + teamID +
                          " ORDER BY t2.StateTime DESC";

                fightReceiveList = context.Database.SqlQuery<FightStateDetailModel>(sql)
                                 .Skip((fight.StartPage - 1) * fight.PageCount)
                                 .Take(fight.PageCount).ToList();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
            }

            returnResult.Add(message);
            returnResult.Add(fightReceiveList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
