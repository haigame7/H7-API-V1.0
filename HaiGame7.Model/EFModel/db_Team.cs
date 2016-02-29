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
    
    public partial class db_Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public db_Team()
        {
            this.db_DateFight = new HashSet<db_DateFight>();
            this.db_DateFight1 = new HashSet<db_DateFight>();
            this.db_GameRecord = new HashSet<db_GameRecord>();
            this.db_TeamUser = new HashSet<db_TeamUser>();
        }
    
        public int TeamID { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public string TeamPicture { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public string TeamType { get; set; }
        public Nullable<int> State { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> WinCount { get; set; }
        public Nullable<int> LoseCount { get; set; }
        public Nullable<int> FollowCount { get; set; }
        public Nullable<int> FightScore { get; set; }
        public Nullable<int> Asset { get; set; }
        public Nullable<int> IsDeault { get; set; }
        public byte[] SysTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_DateFight> db_DateFight { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_DateFight> db_DateFight1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_GameRecord> db_GameRecord { get; set; }
        public virtual db_User db_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_TeamUser> db_TeamUser { get; set; }
    }
}
