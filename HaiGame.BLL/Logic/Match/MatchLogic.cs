using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using HaiGame7.BLL.Enum;
using System;
using HaiGame7.BLL.Logic.Common;

namespace HaiGame7.BLL
{
    public class MatchLogic
    {
        #region 赛事列表
        public string MatchList()
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<MatchModel> matchList = new List<MatchModel>();
            //赛事列表信息
            using (HaiGame7Entities context=new HaiGame7Entities())
            {
                var sql = "SELECT" +
                          " t1.GameID as MatchID," +
                          " t1.GameName as MatchName," +
                          " t1.Introduce as Introduce," +
                          " t1.ShowPicture" +
                          " FROM db_Game t1"+
                          " WHERE t1.State =0";

                matchList = context.Database.SqlQuery<MatchModel>(sql)
                                 .ToList();
            }
            message.MessageCode = MESSAGE.OK_CODE;
            message.Message = MESSAGE.OK;
            returnResult.Add(message);
            returnResult.Add(matchList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 主播列表
        public string BoBoList(MatchModel match)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<BoBoModel> boboList = new List<BoBoModel>();
            
            //主播列表
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var sql = "SELECT" +
                          " t1.GameID as MatchID," +
                          " t3.GameID as GameID," +
                          " t3.BoBoID as BoBoID," +
                          " t2.TalkShow as TalkShow," +
                          " t2.Count," +
                          " t2.TalkShow," +
                          " t3.Name," +
                          " t3.UserPicture," +
                          " t3.Sex," +
                          " t3.Introduce" +
                          " FROM db_Game t1" +
                          " JOIN db_GameBoBo t2 ON t1.GameID=t2.GameID" +
                          " JOIN db_BoBo t3 ON t2.BoBoID=t3.BoBoID" +
                          " WHERE t1.State =0 AND t1.GameID="+ match.MatchID;

                boboList = context.Database.SqlQuery<BoBoModel>(sql)
                                 .ToList();
            }
            message.MessageCode = MESSAGE.OK_CODE;
            message.Message = MESSAGE.OK;
            returnResult.Add(message);
            returnResult.Add(boboList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 主播已报名名额
        public string BoBoCount(MatchParameterModel match)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            BoBoCountModel joinCount = new BoBoCountModel();

            //主播列表
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var sql = "SELECT" +
                          " Count(t1.GameRecordID) as JoinCount" +
                          " FROM db_GameRecord t1" +
                          " WHERE t1.GameID ="+ match.MatchID+ " AND t1.BoBoID=" + match.BoBoID;

                joinCount = context.Database.SqlQuery<BoBoCountModel>(sql)
                                 .FirstOrDefault();
            }
            message.MessageCode = MESSAGE.OK_CODE;
            message.Message = MESSAGE.OK;
            returnResult.Add(message);
            returnResult.Add(joinCount);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 报名参赛
        public string JoinMatch(MatchParameter2Model match)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            //报名参赛
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                db_User user = User.GetUserByPhoneNumber(match.PhoneNumber);
                if(user!=null)
                {
                    db_GameRecord gameRecord = new db_GameRecord();
                    gameRecord.BoBoID = match.BoBoID;
                    gameRecord.GameID = match.MatchID;
                    gameRecord.TeamID = match.TeamID;
                    gameRecord.ApplyTIme = DateTime.Now;
                    gameRecord.UserID = user.UserID;
                    gameRecord.IsCancel = 0;
                    gameRecord.State = 0;
                    context.db_GameRecord.Add(gameRecord);
                    context.SaveChanges();
                }
                
            }
            message.MessageCode = MESSAGE.OK_CODE;
            message.Message = MESSAGE.OK;
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我的已报名信息
        public string MyJoinMatch(MatchParameter2Model match)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            MyBoBoModel myBobo = new MyBoBoModel();

            message.MessageCode = MESSAGE.NOJOINMATCH_CODE;
            message.Message = MESSAGE.NOJOINMATCH;
            //报名参赛
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                db_User user = User.GetUserByPhoneNumber(match.PhoneNumber);
                if (user != null)
                {
                    var matchRecord=context.db_GameRecord
                        .Where(c => c.GameID == match.MatchID)
                        .Where(c => c.TeamID == match.TeamID)
                        .FirstOrDefault();
                    if (matchRecord != null)
                    {
                        var bobo=context.db_BoBo
                        .Where(c => c.BoBoID == matchRecord.BoBoID)
                        .FirstOrDefault();
                        if (bobo!=null)
                        {
                            myBobo.Name = bobo.Name;
                            myBobo.ApplyTime = ((DateTime)matchRecord.ApplyTIme).ToString("yyyy-MM-dd hh:mm:ss");
                            message.MessageCode = MESSAGE.OK_CODE;
                            message.Message = MESSAGE.OK;
                        }
                    }
                }
            }

            returnResult.Add(message);
            returnResult.Add(myBobo);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
