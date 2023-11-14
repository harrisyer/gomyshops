using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("StatusSU")]
    public partial class StatusSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }

        [StringLength(255)]
        public string StatusDesc { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }


    }//end class
}//end namespace
