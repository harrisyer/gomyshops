using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    [Table("LookupIndustrySU")]
    public partial class LookupIndustrySU
    {
        [Key]
        public int Id { get; set; }

        public int IndustryCode { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public int Status { get; set; }
    }//end class
}//end namespace
