//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HaiGame7.Model.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class db_TeamUser
    {
        public int ID { get; set; }
        public Nullable<int> TeamID { get; set; }
        public Nullable<int> UserID { get; set; }
        public byte[] SysTime { get; set; }
    
        public virtual db_Team db_Team { get; set; }
    }
}
