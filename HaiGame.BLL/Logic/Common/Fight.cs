using HaiGame7.BLL.Enum;
using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.BLL.Logic.Common
{
    public class Fight
    {
        #region 拼接约战动态
        public static string FightState(FightStateDetailModel fightStateDetail)
        {
            string fightState = "";
            switch (fightStateDetail.CurrentState)
            {
                case "发起挑战":
                    fightState = "战队【" + fightStateDetail.STeamName + "】向战队【" + fightStateDetail.ETeamName + "】发出约战";
                    break;
                case "已应战":
                    fightState = "战队【" + fightStateDetail.ETeamName + "】接受战队【" + fightStateDetail.STeamName + "】的约战";
                    break;
                case "已认怂":
                    fightState = "战队【" + fightStateDetail.ETeamName + "】向战队【" + fightStateDetail.STeamName + "】认怂";
                    break;
                case "挑战成功":
                    fightState = "战队【" + fightStateDetail.STeamName + "】已战胜【" + fightStateDetail.ETeamName + "】，赢取"+ fightStateDetail.Money+"氦金";
                    break;
                case "守擂成功":
                    fightState = "战队【" + fightStateDetail.ETeamName + "】已战胜【" + fightStateDetail.STeamName + "】，赢取" + fightStateDetail.Money + "氦金";
                    break;

            }
            
            return fightState;
        }
        #endregion

        #region 判断角色，队员不能发起约战
        public static bool IsTeamCreater(int userID,int teamID)
        {
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var team=context.db_Team.
                                Where(c => c.CreateUserID == userID).
                                Where(c => c.TeamID == teamID).
                                FirstOrDefault();
                if (team==null)
                {
                    //队员
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 判断是否超出每日限额
        public static bool IsDailyLimit(int userID, int teamID)
        {
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var team = context.db_Team.
                                Where(c => c.CreateUserID == userID).
                                Where(c => c.TeamID == teamID).
                                FirstOrDefault();
                if (team == null)
                {
                    //队员
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 判断是否与对方有未完成的约战
        public static bool IsFinished(int userID, int teamID)
        {
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                var team = context.db_Team.
                                Where(c => c.CreateUserID == userID).
                                Where(c => c.TeamID == teamID).
                                FirstOrDefault();
                if (team == null)
                {
                    //队员
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 发起约战前提条件判断
        public static MessageModel IsChallenge(int userID, int teamID)
        {
            MessageModel message = new MessageModel();
            //判断角色，队员不能发起约战
            if (IsTeamCreater(userID, teamID) == false)
            {
                message.Message = MESSAGE.USERCHALLENGE;
                message.MessageCode = MESSAGE.USERCHALLENGE_CODE;
                return message;
            }

            //判断是否超出每日限额
            if (IsDailyLimit(userID, teamID) == false)
            {
                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                return message;
            }
            //判断是否与对方有未完成的约战
            if (IsFinished(userID, teamID) == false)
            {
                message.Message = MESSAGE.OK;
                message.MessageCode = MESSAGE.OK_CODE;
                return message;
            }
            message.Message= MESSAGE.OK;
            message.MessageCode = MESSAGE.OK_CODE;
            return message;
        }
        #endregion
    }
}
