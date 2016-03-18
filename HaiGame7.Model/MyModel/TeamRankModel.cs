using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.Model.MyModel
{
    public class TeamRankModel
    {
        public string TeamName { get; set; }//战队名称
        public string TeamDescription { get; set; }//战队口号
        public int HotScore { get; set; }//热度
        public int FightScore { get; set; }//战斗力
        public int Asset { get; set; }//氦金
    }
}
