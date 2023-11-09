using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("MccCodeSU")]
    public partial class MccCodeSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int IndustryCode { get; set; }

        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings =true)]
        [StringLength(500)]
        public string MccDescription { get; set; }

    }//end class
}//end namespace
