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
                    fightState = "战队【" + fightStateDetail.STeamName + "】已战胜【" + fightStateDetail.ETeamName + "】发出约战";
                    break;
                case "守擂成功":
                    fightState = "战队【" + fightStateDetail.ETeamName + "】已战胜【" + fightStateDetail.STeamName + "】发出约战";
                    break;

            }
            
            return fightState;
        }
        #endregion
    }
}
