using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AspNetUserLogins")]
    public partial class AspNetUserLogin
    {
        //[Key]
        //[Column(Order = 0)]
        [StringLength(128)]
        public string LoginProvider { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(128)]
        public string ProviderKey { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [StringLength(128)]
        public string UserId { get; set; }

        [NotMapped]
        public virtual AspNetUser AspNetUser { get; set; }
    }//end class
}//end namespace
