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
    
    public partial class db_CommonUseHero
    {
        public int CommonHeroID { get; set; }
        public Nullable<int> GamePlatformID { get; set; }
        public string HeroName { get; set; }
        public string NickName { get; set; }
        public string HeroImage { get; set; }
        public Nullable<int> UseCount { get; set; }
        public Nullable<decimal> HeroOdds { get; set; }
        public Nullable<decimal> HeroKDA { get; set; }
        public string HeroKDADetail { get; set; }
        public Nullable<decimal> AverageKDA { get; set; }
        public Nullable<int> WinCount { get; set; }
        public Nullable<int> LoseCount { get; set; }
        public byte[] System { get; set; }
    
        public virtual db_GameInfoofPlatform db_GameInfoofPlatform { get; set; }
    }
}
