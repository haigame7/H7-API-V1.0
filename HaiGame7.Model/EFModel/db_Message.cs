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
    
    public partial class db_Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public db_Message()
        {
            this.db_SysMessage = new HashSet<db_SysMessage>();
        }
    
        public int MID { get; set; }
        public Nullable<int> SendID { get; set; }
        public string SendName { get; set; }
        public Nullable<int> ReceiveID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public Nullable<int> State { get; set; }
        public string MessageType { get; set; }
        public byte[] SysTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_SysMessage> db_SysMessage { get; set; }
    }
}
