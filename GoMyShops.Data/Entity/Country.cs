using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Country")]
    public partial class Country
    {
        [Key]
        [StringLength(5)]
        public string CountryCode { get; set; }

        [StringLength(5)]
        public string CountryCode2 { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(5)]
        public string CurrencyCode { get; set; }

        public int Sequence { get; set; }

        [StringLength(1)]
        public string Status { get; set; }


        //[Required]
        //[StringLength(20)]
        //public string CreatedBy { get; set; }

        //public DateTime CreatedTime { get; set; }

        //[StringLength(20)]
        //public string ModifiedBy { get; set; }

        //public DateTime? ModifiedTime { get; set; }
    }//end class
}//end namespace
