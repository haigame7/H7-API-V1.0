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
    
    public partial class db_Contribute
    {
        public int ContributeID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Catalog { get; set; }
        public Nullable<System.DateTime> ContributeTime { get; set; }
        public string State { get; set; }
        public byte[] SysTime { get; set; }
    
        public virtual db_User db_User { get; set; }
    }
}
