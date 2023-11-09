using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("AspNetRoles")]
    public partial class AspNetRole
    {
        [Key]
        //[Required]
        [StringLength(128)]
        public string Id { get; set; }

        //public string UserId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }//end class
}//end namespace