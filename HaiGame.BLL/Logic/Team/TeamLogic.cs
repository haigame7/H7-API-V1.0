using HaiGame7.BLL.Enum;
using HaiGame7.BLL.Logic.Common;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HaiGame7.BLL
{
    public class TeamLogic
    {
        #region 创建战队
        public string Create(SimpleTeamModel team)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断战队名称是否存在
                var count=context.db_Team.
                    Where(c => c.TeamName == team.TeamName.Trim()).
                    Where(c => c.State == 0).
                    ToList().Count;
                if (count==0)
                {
                    //判断是否有权限创建
                    var creater = context.db_User.Where(c => c.PhoneNumber == team.Creater).FirstOrDefault();
                    var existCount = context.db_TeamUser.Where(c => c.UserID == creater.UserID).ToList().Count;
                    if(existCount==0)
                    {
                        db_Team teamInsert = new db_Team();
                        teamInsert.TeamName = team.TeamName.Trim();
                        teamInsert.TeamPicture = team.TeamLogo;
                        teamInsert.TeamType = team.TeamType;
                        teamInsert.State = 0;
                        teamInsert.CreateTime = DateTime.Now;
                        teamInsert.IsDeault = 0;
                        //添加到db_Team表
                        context.db_Team.Add(teamInsert);
                        context.SaveChanges();
                    }
                    else
                    {
                        message.Message = MESSAGE.TEAMUSER;
                        message.MessageCode = MESSAGE.TEAMUSER_CODE;
                    } 
                }
                else
                {
                    message.Message = MESSAGE.TEAMEXIST;
                    message.MessageCode = MESSAGE.TEAMEXIST_CODE;
                }
                
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 更新战队
        public string Update(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断战队名称是否存在
                //判断是否有权限创建
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 解散战队
        public string Delete(SimpleUserModel user)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断战队名称是否存在
                //判断是否有权限创建
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取我的默认战队
        public string MyTeam(SimpleTeamModel team)
        {
            string result = "";
            MessageModel message = new MessageModel();
            TeamModel teamInfo = new TeamModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //三种情况：1.不属于任何战队，也没有自己的战队。2.是某战队的队员。 3.是某战队的队长
                db_User user = context.db_User.Where(c => c.PhoneNumber == team.Creater).FirstOrDefault();
                if (user != null)
                {
                    //判断用户是否加入战队或创建战队
                    bool isCreateOrJoinTeam = Team.IsCreateOrJoinTeam(user.UserID, context);
                    if (isCreateOrJoinTeam == false)
                    {
                        //不属于任何战队
                        message.Message = MESSAGE.NOTEAM;
                        message.MessageCode = MESSAGE.NOTEAM_CODE;
                    }
                    else
                    {
                        //获取用户的战队信息，通过Role字段区别是队员还是队长
                        teamInfo = Team.MyTeam(user.UserID, context);
                    }
                }
                else
                {
                    //无此用户
                    message.Message = MESSAGE.NOUSER;
                    message.MessageCode = MESSAGE.NOUSER_CODE;
                }
                returnResult.Add(message);
                returnResult.Add(teamInfo);
            }

            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 获取战队列表
        public string TeamList(TeamListParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<TeamModel> teamList = new List<TeamModel>();
            HashSet<object> returnResult = new HashSet<object>();

            //不同参数类型返回不同结果
            switch (para.Type.ToLower())
            {
                case "userfightscore":
                    teamList=Team.TeamListByUserFightScore(para);
                    break;
                case "teamfightscore":
                    teamList = Team.TeamListByTeamFightScore(para);
                    break;
                case "createdate":
                    teamList = Team.TeamListByCreateDate(para);
                    break;
            }
            returnResult.Add(message);
            returnResult.Add(teamList);
            
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 根据战队名称获取战队信息
        public string GetTeambyName(string teamName)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断战队名称是否存在
                //判断是否有权限创建
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion

        #region 根据手机号称获取战队
        public string GetTeambyPhone(string phoneNumber)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                
                //判断战队名称是否存在
                //判断是否有权限创建
            }
            result = jss.Serialize(message);
            return result;
        }
        #endregion

        #region 招募信息列表
        public string RecruitList(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<RecruitModel> recruitList;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                string sql;
                if (para.UserID == 0)
                {
                    sql = "SELECT" +
                          " t2.TeamID as TeamID," +
                          " t2.TeamName as TeamName," +
                          " t2.TeamPicture as TeamLogo," +
                          " t2.TeamDescription as TeamDescription," +
                          " t2.FightScore," +
                          " CONVERT(varchar(100), t1.RecruitTime, 20) as RecruitTime," +
                          " t1.Content as RecruitContent" +
                          " FROM db_Recruit t1 " +
                          " LEFT JOIN db_Team t2 on t1.TeamID = t2.TeamID" +
                          " WHERE t2.State = 0" +
                          " ORDER BY t1.RecruitTime DESC ";
                }
                else
                {
                    string teamID = Team.MyAllTeamID(para.UserID);
                    //战队名称，战队logo，申请日期，战斗力，氦金，状态
                    sql = "SELECT" +
                              " t2.TeamID as TeamID," +
                              " t2.TeamName as TeamName," +
                              " t2.TeamPicture as TeamLogo," +
                              " t2.TeamDescription as TeamDescription," +
                              " t2.FightScore," +
                              " CONVERT(varchar(100), t1.RecruitTime, 20) as RecruitTime," +
                              " t1.Content as RecruitContent" +
                              " FROM db_Recruit t1 " +
                              " LEFT JOIN db_Team t2 on t1.TeamID = t2.TeamID" +
                              " WHERE t2.State = 0 AND t1.TeamID NOT IN" + teamID +
                              " ORDER BY t1.RecruitTime DESC ";
                }
                recruitList = context.Database.SqlQuery<RecruitModel>(sql)
                                 .Skip((para.StartPage - 1) * para.PageCount)
                                 .Take(para.PageCount).ToList();
            }
            returnResult.Add(message);
            returnResult.Add(recruitList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 申请加入战队
        public string ApplyTeam(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断角色是否是队长，队长不可申请加入其它战队
                //判断是否参加其它战队，每个队员只可以参加一只战队
                if(Team.IsCreateOrJoinTeam(para.UserID, context)== false)
                {
                    //判断是否向该战队发出过申请
                    int applyCount=context.db_Message.Where(c => c.SendID == para.UserID)
                                      .Where(c => c.ReceiveID == para.TeamID)
                                      .ToList().Count;
                    //申请加入
                    if (applyCount>0)
                    {
                        message.MessageCode = MESSAGE.JIONEDTEAM_CODE;
                        message.Message = MESSAGE.JIONEDTEAM;
                    }
                    else
                    {
                        db_Message applyMessage = new db_Message();
                        applyMessage.SendID = para.UserID;
                        applyMessage.ReceiveID = para.TeamID;
                        applyMessage.SendTime = DateTime.Now;
                        applyMessage.State = 1;
                        applyMessage.MessageType = "加入战队";
                    }
                }
                else
                {
                    message.MessageCode = MESSAGE.JIONTEAM_CODE;
                    message.Message = MESSAGE.JIONTEAM;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 我的申请列表
        public string ApplyTeamList(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<MyApplyTeamModel> teamList;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //从Message表取数据，我的申请条件：state=1 and UserID=我的ID。2.申请加入条件：state=1 and TeamID=我的默认战队ID。

                //战队名称，战队logo，申请日期，战斗力，氦金，状态
                var sql = "SELECT" +
                          " t1.ReceiveID as TeamID," +
                          " t1.MessageType as State," +
                          " t2.TeamName as TeamName," +
                          " t2.TeamPicture as TeamLogo," +
                          " t2.TeamDescription as TeamDescription," +
                          " t2.FightScore," +
                          " CONVERT(varchar(100), t1.SendTime, 20) as SendTime," +
                          " t2.Asset" +
                          " FROM db_Message t1 " +
                          " LEFT JOIN db_Team t2 on t1.ReceiveID = t2.TeamID" +
                          " WHERE t1.State = 1 AND t1.SendID=" + para.UserID+
                          " ORDER BY t1.SendTime DESC ";

                teamList = context.Database.SqlQuery<MyApplyTeamModel>(sql)
                                 .Skip((para.StartPage - 1) * para.PageCount)
                                 .Take(para.PageCount).ToList();
            }
            returnResult.Add(message);
            returnResult.Add(teamList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region  我的受邀列表
        public string InvitedTeamList(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<MyApplyTeamModel> teamList;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //从Message表取数据，我的申请条件：state=1 and UserID=我的ID。2.申请加入条件：state=1 and TeamID=我的默认战队ID。

                //战队名称，战队logo，申请日期，战斗力，氦金，状态
                var sql = "SELECT" +
                          " t1.SendID as TeamID," +
                          " t1.MessageType as State," +
                          " t2.TeamName as TeamName," +
                          " t2.TeamPicture as TeamLogo," +
                          " t2.TeamDescription as TeamDescription," +
                          " t2.FightScore," +
                          " CONVERT(varchar(100), t1.SendTime, 20) as SendTime," +
                          " t2.Asset" +
                          " FROM db_Message t1 " +
                          " LEFT JOIN db_Team t2 on t1.SendID = t2.TeamID" +
                          " WHERE t1.State = 2 AND t1.ReceiveID=" + para.UserID +
                          " ORDER BY t1.SendTime DESC ";

                teamList = context.Database.SqlQuery<MyApplyTeamModel>(sql)
                                 .Skip((para.StartPage - 1) * para.PageCount)
                                 .Take(para.PageCount).ToList();
            }
            returnResult.Add(message);
            returnResult.Add(teamList);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 发布招募
        public string SendRecruit(RecruitParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //db_Recruit表插入或更新一条数据
                var recruit=context.db_Recruit.Where(c => c.TeamID == para.TeamID).FirstOrDefault();
                if (recruit == null)
                {
                    db_Recruit record = new db_Recruit();
                    record.TeamID = para.TeamID;
                    record.Content = para.Content;
                    record.RecruitTime = DateTime.Now;
                    context.db_Recruit.Add(record);
                }
                else
                {
                    recruit.Content = para.Content;
                    recruit.RecruitTime = DateTime.Now;
                }
                context.SaveChanges();
            }
            message.Message = MESSAGE.OK;
            message.MessageCode = MESSAGE.OK_CODE;
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 邀请队员
        public string InviteUser(InviteUserParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //判断是否发出过邀请
                int inviteCount = context.db_Message.Where(c => c.SendID == para.TeamID)
                                      .Where(c => c.ReceiveID == para.UserID)
                                      .ToList().Count;
                if (inviteCount > 0)
                {
                    message.MessageCode = MESSAGE.INVITEUSER_CODE;
                    message.Message = MESSAGE.INVITEUSER;
                }
                else
                {
                    //db_Message表插入一条数据
                    db_Message mes = new db_Message();
                    mes.SendID = para.TeamID;
                    mes.ReceiveID = para.UserID;
                    mes.State = 2;
                    mes.Title = "招募队员";
                    mes.SendTime = DateTime.Now;
                    context.db_Message.Add(mes);
                    context.SaveChanges();
                    message.Message = MESSAGE.OK;
                    message.MessageCode = MESSAGE.OK_CODE;
                }
            }
            returnResult.Add(message);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 发出邀请列表
        public string InvitedUserList(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<User2Model> userInfo;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //从Message表取数据，发出申请条件：state=2 and SendID=TeamID。
                var sql = "SELECT t2.UserID,t2.PhoneNumber,t2.UserWebNickName," +
                         "  t2.UserWebPicture,t2.UserName,t2.Address," +
                         "  t1.MessageType as State," +
                         "  CONVERT(varchar(100), t1.SendTime, 20) as SendTime," +
                         "  t2.Sex,CONVERT(varchar(100), t2.Birthday, 23) as Birthday,t2.Hobby" +
                         "  FROM" +
                         "  db_Message t1" +
                         "  LEFT JOIN db_User t2 ON t1.ReceiveID = t2.UserID" +
                         "  WHERE state=2 AND t1.SendID =" + para.TeamID +
                         "  ORDER BY t1.SendTime DESC";

                userInfo = context.Database.SqlQuery<User2Model>(sql)
                                 .Skip((para.StartPage - 1) * para.PageCount)
                                 .Take(para.PageCount).ToList();
                //循环user，添加擅长英雄图标
                for (int i = 0; i < userInfo.Count; i++)
                {
                    //氦金
                    userInfo[i].Asset = User.GetAssetByUserID(userInfo[i].UserID);
                    //战斗力
                    userInfo[i].GamePower = User.GetGamePowerByUserID(userInfo[i].UserID);
                    userInfo[i].HeroImage = User.GetHeroImgeByUserID(userInfo[i].UserID);
                }
            }
            returnResult.Add(message);
            returnResult.Add(userInfo);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion

        #region 申请加入列表
        public string ApplyUserList(ApplyTeamParameterModel para)
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            List<User2Model> userInfo;

            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                //从Message表取数据，发出申请条件：state=1 and ReceiveID=TeamID。
                var sql = "SELECT t2.UserID,t2.PhoneNumber,t2.UserWebNickName," +
                         "  t2.UserWebPicture,t2.UserName,t2.Address," +
                         "  t1.MessageType as State," +
                         "  CONVERT(varchar(100), t1.SendTime, 20) as SendTime," +
                         "  t2.Sex,CONVERT(varchar(100), t2.Birthday, 23) as Birthday,t2.Hobby" +
                         "  FROM" +
                         "  db_Message t1" +
                         "  LEFT JOIN db_User t2 ON t1.SendID = t2.UserID" +
                         "  WHERE state=1 AND t1.ReceiveID =" + para.TeamID +
                         "  ORDER BY t1.SendTime DESC";

                userInfo = context.Database.SqlQuery<User2Model>(sql)
                                 .Skip((para.StartPage - 1) * para.PageCount)
                                 .Take(para.PageCount).ToList();
                //循环user，添加擅长英雄图标
                for (int i = 0; i < userInfo.Count; i++)
                {
                    //氦金
                    userInfo[i].Asset = User.GetAssetByUserID(userInfo[i].UserID);
                    //战斗力
                    userInfo[i].GamePower = User.GetGamePowerByUserID(userInfo[i].UserID);
                    userInfo[i].HeroImage = User.GetHeroImgeByUserID(userInfo[i].UserID);
                }
            }
            returnResult.Add(message);
            returnResult.Add(userInfo);
            result = jss.Serialize(returnResult);
            return result;
        }
        #endregion
    }
}
