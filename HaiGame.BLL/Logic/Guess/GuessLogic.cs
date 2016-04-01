using HaiGame7.BLL.Enum;
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
    public class GuessLogic
    {
        #region 竞猜列表
        public string GuessList()
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            //获取竞猜列表
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //联合查询
                var sql = "SELECT"+
                          "  t1.GuessID as GuessID," +
                          "  t2.GameStage as MatchName," +
                          "  t2.HomeTeamID as STeamID,"+
                          "  t2.CustomerTeamID as ETeamID,"+
                          "  t3.TeamName as STeamName,"+
                          "  t3.TeamPicture as STeamLogo," +
                          " t4.TeamName as ETeamName," +
                          " t4.TeamPicture as ETeaLogo," +
                          " CONVERT(varchar(100), t2.EndTime, 20) as MatchTime," +
                          " t1.GuessName as GuessType,"+
                          " t1.STeamOdds as STeamOdds,"+
                          " t1.ETeamOdds as ETeamOdds,"+
                          " (SELECT COUNT(s.GuessRecordID) FROM db_GuessRecord s WHERE s.GuessID = t1.GuessID) as AllUser,"+
                          " (SELECT COUNT(s.BetMoney) FROM db_GuessRecord s WHERE s.GuessID = t1.GuessID) as AllMoney"+
                          " FROM"+
                          " db_MatchGuess t1"+
                          " LEFT JOIN db_FightResult t2 ON t1.ResultID = t2.ResultID" +
                          " LEFT JOIN db_Team t3 ON t2.HomeTeamID = t3.TeamID" +
                          " LEFT JOIN db_Team t4 ON t2.CustomerTeamID = t4.TeamID";

                var guessList = context.Database.SqlQuery<GuessModel>(sql)
                                 .ToList();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                returnResult.Add(message);
                returnResult.Add(guessList);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 押注
        public string Bet(GuessRecordModel guess)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            //获取竞猜列表
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //向db_GuessRecord表插入数据
                db_GuessRecord guessInsert = new db_GuessRecord();
                guessInsert.BetMoney = guess.Money;
                guessInsert.GuessID = guess.GuessID;
                guessInsert.GuessTime = DateTime.Now;
                guessInsert.GuessType = 0;
                guessInsert.Odds = guess.Odds;
                guessInsert.OptionID = guess.TeamID;
                guessInsert.UserID = guess.UserID;
                context.db_GuessRecord.Add(guessInsert);
                context.SaveChanges();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                returnResult.Add(message);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我的竞猜列表
        public string MyGuessList(GuessParameterModel guess)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            //获取竞猜列表
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                string where = "";
                if (guess.GuessID==0)
                {
                    where = " WHERE t1.UserID="+ guess.UserID;
                }
                else
                {
                    where = " WHERE t1.UserID=" + guess.UserID+ " AND t1.GuessID=" + guess.GuessID;
                }
                //联合查询
                var sql = "SELECT"+
                          "  t3.GameStage as MatchName,"+
                          "  CONVERT(varchar(100), t3.EndTime, 20) as EndTime," +
                          "  CONVERT(varchar(100), t1.GuessTime, 20) as GuessTime," + 
                          "  t1.BetMoney," +
                          "  t1.Odds,"+
                          "  t3.Result as Result," +
                          "  t4.TeamID as STeamID," +
                          "  t4.TeamName as STeamName,"+
                          "  t4.TeamPicture as STeamLogo,"+
                          "  t5.TeamID as ETeamID,"+
                          "  t5.TeamName as ETeamName,"+
                          "  t5.TeamPicture as STeamLogo,"+
                          "  t6.TeamID as BetTeamID,"+
                          "  t6.TeamName as BetTeamName,"+
                          "  t6.TeamPicture as BetTeamLogo"+
                          "  FROM"+
                          "  db_GuessRecord t1"+
                          "  LEFT JOIN db_MatchGuess t2 ON t1.GuessID = t2.GuessID"+
                          "  LEFT JOIN db_FightResult t3 ON t2.ResultID = t2.ResultID"+
                          "  LEFT JOIN db_Team t4 ON t3.HomeTeamID = t4.TeamID"+
                          "  LEFT JOIN db_Team t5 ON t3.CustomerTeamID = t5.TeamID"+
                          "  LEFT JOIN db_Team t6 ON t1.OptionID = t5.TeamID" + where;

                var guessList = context.Database.SqlQuery<Guess2Model>(sql)
                                 .Skip((guess.StartPage - 1) * guess.PageCount)
                                 .Take(guess.PageCount).ToList();

                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                returnResult.Add(message);
                returnResult.Add(guessList);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
