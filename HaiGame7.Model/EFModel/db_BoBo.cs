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
    
    public partial class db_BoBo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public db_BoBo()
        {
            this.db_GameBoBo = new HashSet<db_GameBoBo>();
        }
    
        public int BoBoID { get; set; }
        public string GameID { get; set; }
        public string Name { get; set; }
        public string UserPicture { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Introduce { get; set; }
        public string 备注 { get; set; }
        public byte[] SysTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_GameBoBo> db_GameBoBo { get; set; }
    }
}
