using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AspNetUserClaims")]
    public partial class AspNetUserClaim
    {
        [Key]
        //[Required]
        [StringLength(128)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [NotMapped]
        public virtual AspNetUser AspNetUser { get; set; }
    }//end class
}//end namespace