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

        #region 申请加入战队
        public string ApplyTeam(string teamName)
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

        #region 我的申请记录
        public string MyApply(string phoneNumber)
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

        #region 我的受邀记录
        public string MyInvited(string phoneNumber)
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

        #region 战队发出邀请记录
        public string TeamRecruit(string phoneNumber)
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

        #region 战队接收邀请记录
        public string TeamApply(string phoneNumber)
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
    }
}
