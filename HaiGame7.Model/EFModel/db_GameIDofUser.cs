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
    
    public partial class db_GameIDofUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public db_GameIDofUser()
        {
            this.db_GameInfoofPlatform = new HashSet<db_GameInfoofPlatform>();
        }
    
        public int UGID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string GameID { get; set; }
        public string GameArea { get; set; }
        public string GameType { get; set; }
        public Nullable<int> CertifyState { get; set; }
        public string CertifyName { get; set; }
        public Nullable<System.DateTime> ApplyCertifyTime { get; set; }
        public byte[] System { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_GameInfoofPlatform> db_GameInfoofPlatform { get; set; }
        public virtual db_User db_User { get; set; }
    }
}
