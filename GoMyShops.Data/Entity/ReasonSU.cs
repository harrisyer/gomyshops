using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("ReasonSU")]
    public class ReasonSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string ReasonType { get; set; }

        [Required]
        [StringLength(20)]
        public string ReasonCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        public int Status { get; set; }
    }
}
