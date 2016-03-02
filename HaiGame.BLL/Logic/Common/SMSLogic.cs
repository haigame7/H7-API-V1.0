using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaiGame7.BLL.Enum;

namespace HaiGame7.BLL.Logic.Common
{
    public class SMSLogic
    {
        public static Dictionary<string,object> SendSMS(string phoneNumber, string randomNum)
        {
            Dictionary<string, object> retData=new Dictionary<string, object>();
            string[] data = { randomNum, SMS.TIME_OUT };
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init(SMS.ADDRESS,SMS.PORT);
            api.setAccount(SMS.ACCOUNT_SID, SMS.ACCOUNT_TOKEN);
            api.setAppId(SMS.APP_ID);
            if (isInit)
            {
                retData = api.SendTemplateSMS(phoneNumber, SMS.TEMPLATE_ID, data);
            }
            return retData;
        }
    }
}
