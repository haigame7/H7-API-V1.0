using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.BLL.Logic.Common
{
    public class Team
    {
        #region 判断用户是否加入战队或创建战队
        public static bool IsCreateOrJoinTeam(int userID, HaiGame7Entities context)
        {
            db_Team team=context.db_Team.
                Where(c => c.CreateUserID == userID).
                Where(c => c.State == 0).
                FirstOrDefault();
            if (team==null)
            {
                db_TeamUser teamUser=context.db_TeamUser.Where(c => c.UserID == userID).FirstOrDefault();
                if (teamUser == null)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 获取我的战队信息
        public static TeamModel MyTeam(int userID, HaiGame7Entities context)
        {
            TeamModel myTeam = new TeamModel();

            db_Team team = context.db_Team.
                Where(c => c.CreateUserID == userID).
                Where(c => c.State == 0).
                Where(c => c.IsDeault == 0).
                FirstOrDefault();
            if (team == null)
            {
                db_TeamUser teamUser = context.db_TeamUser.Where(c => c.UserID == userID).FirstOrDefault();
                db_Team team2 = context.db_Team.
                Where(c => c.TeamID == team.TeamID).
                Where(c => c.State == 0).
                FirstOrDefault();
                if (team2!=null)
                {
                    myTeam.Asset = team2.Asset;
                    myTeam.Creater = team2.CreateUserID;
                    myTeam.CreateTime = ((DateTime)team2.CreateTime).ToString("yyyy-MM-dd");
                    myTeam.FightScore= team2.FightScore;
                    myTeam.FollowCount = team2.FollowCount;
                    myTeam.IsDeault = team2.IsDeault;
                    myTeam.LoseCount = team2.LoseCount;
                    myTeam.Role = "teamuser";
                    myTeam.TeamLogo = team2.TeamPicture;
                    myTeam.TeamName = team2.TeamName;
                    myTeam.TeamType = team2.TeamType;
                    myTeam.TeamDescription = team2.TeamDescription;
                    myTeam.WinCount = team2.WinCount;
                }
            }
            else
            {
                myTeam.Asset = team.Asset;
                myTeam.Creater = team.CreateUserID;
                myTeam.CreateTime = ((DateTime)team.CreateTime).ToString("yyyy-MM-dd");
                myTeam.FightScore = team.FightScore;
                myTeam.FollowCount = team.FollowCount;
                myTeam.IsDeault = team.IsDeault;
                myTeam.LoseCount = team.LoseCount;
                myTeam.Role = "teamcreater";
                myTeam.TeamLogo = team.TeamPicture;
                myTeam.TeamName = team.TeamName;
                myTeam.TeamType = team.TeamType;
                myTeam.TeamDescription = team.TeamDescription;
                myTeam.WinCount = team.WinCount;
            }

            return myTeam;
        }
        #endregion

        #region 用户个人战斗力匹配战队
        public static List<TeamModel> TeamListByUserFightScore(TeamListParameterModel para)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 获取战队列表
                var sql = "SELECT" +
                    " t1.CreateUserID as Creater," +
                    " t1.TeamName," +
                    " t1.TeamPicture as TeamLogo," +
                    " t1.TeamDescription," +
                    " t1.TeamType," +
                    " (CASE WHEN t1.FightScore IS NULL THEN 0 ELSE t1.FightScore END) as FightScore," +
                    " (CASE WHEN t1.Asset IS NULL THEN 0 ELSE t1.Asset END) as Asset," +
                    " t1.IsDeault," +
                    " (CASE WHEN t1.WinCount IS NULL THEN 0 ELSE t1.WinCount END) as WinCount," +
                    " (CASE WHEN t1.LoseCount IS NULL THEN 0 ELSE t1.LoseCount END) as LoseCount," +
                    " (CASE WHEN t1.FollowCount IS NULL THEN 0 ELSE t1.FollowCount END) as FollowCount," +
                    " CONVERT(varchar(100), t1.CreateTime, 23) as CreateTime" +
                    " FROM" +
                    " db_Team t1" +
                    " ORDER BY t1.CreateTime " + para.Sort;

                teamList = context.Database.SqlQuery<TeamModel>(sql)
                                  .Skip((para.StartPage - 1) * para.PageCount)
                                  .Take(para.PageCount).ToList();
            }
            return teamList; 
        }
        #endregion

        #region 我的战队战斗力匹配战队
        public static List<TeamModel> TeamListByTeamFightScore(TeamListParameterModel para)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 获取战队列表
                var sql = "SELECT" +
                    " t1.CreateUserID as Creater," +
                    " t1.TeamName," +
                    " t1.TeamPicture as TeamLogo," +
                    " t1.TeamDescription," +
                    " t1.TeamType," +
                    " (CASE WHEN t1.FightScore IS NULL THEN 0 ELSE t1.FightScore END) as FightScore," +
                    " (CASE WHEN t1.Asset IS NULL THEN 0 ELSE t1.Asset END) as Asset," +
                    " t1.IsDeault," +
                    " (CASE WHEN t1.WinCount IS NULL THEN 0 ELSE t1.WinCount END) as WinCount," +
                    " (CASE WHEN t1.LoseCount IS NULL THEN 0 ELSE t1.LoseCount END) as LoseCount," +
                    " (CASE WHEN t1.FollowCount IS NULL THEN 0 ELSE t1.FollowCount END) as FollowCount," +
                    " CONVERT(varchar(100), t1.CreateTime, 23) as CreateTime" +
                    " FROM" +
                    " db_Team t1" +
                    
                    " ORDER BY t1.CreateTime " + para.Sort;

                teamList = context.Database.SqlQuery<TeamModel>(sql)
                                  .Skip((para.StartPage - 1) * para.PageCount)
                                  .Take(para.PageCount).ToList();
            }
            return teamList;
        }
        #endregion

        #region 按战队注册时间获取战队列表
        public static List<TeamModel> TeamListByCreateDate(TeamListParameterModel para)
        {
            List<TeamModel> teamList = new List<TeamModel>();
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                // 获取战队列表
                var sql = "SELECT"+
                    " t1.CreateUserID as Creater,"+
                    " t1.TeamName,"+
                    " t1.TeamPicture as TeamLogo,"+
                    " t1.TeamDescription,"+
                    " t1.TeamType,"+
                    " (CASE WHEN t1.FightScore IS NULL THEN 0 ELSE t1.FightScore END) as FightScore," +
                    " (CASE WHEN t1.Asset IS NULL THEN 0 ELSE t1.Asset END) as Asset," +
                    " t1.IsDeault,"+
                    " (CASE WHEN t1.WinCount IS NULL THEN 0 ELSE t1.WinCount END) as WinCount," +
                    " (CASE WHEN t1.LoseCount IS NULL THEN 0 ELSE t1.LoseCount END) as LoseCount," +
                    " (CASE WHEN t1.FollowCount IS NULL THEN 0 ELSE t1.FollowCount END) as FollowCount," +
                    " CONVERT(varchar(100), t1.CreateTime, 23) as CreateTime"+
                    " FROM"+
                    " db_Team t1"+
                    " ORDER BY t1.CreateTime " + para.Sort;

                teamList = context.Database.SqlQuery<TeamModel>(sql)
                                  .Skip((para.StartPage - 1) * para.PageCount)
                                  .Take(para.PageCount).ToList();
            }
            return teamList;
        }
        #endregion
    }
}
