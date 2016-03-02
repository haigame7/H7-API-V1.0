using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaiGame7.BLL.Logic.Common
{
    public class CheckToken
    {
        public static bool IsValid(string token)
        {
            if (token=="ABC12abc")
            {
                return true;
            }
            return false;
        }
        
    }
}
