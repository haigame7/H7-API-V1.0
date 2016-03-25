using HaiGame7.Model.EFModel;
using HaiGame7.Model.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HaiGame7.BLL.Logic.Match
{
    public class MatchLogic
    {
        public string MatchList()
        {
            string result = "";
            MessageModel message = new MessageModel();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            HashSet<object> returnResult = new HashSet<object>();
            //个人排行：昵称，签名，氦金，战斗力，大神系数
            using (HaiGame7Entities context = new HaiGame7Entities())
            {
                
                returnResult.Add(message);
                returnResult.Add(userRank);
            }
            result = jss.Serialize(returnResult);
            return result;
        }
    }
}
