using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("State")]
    public partial class State
    {
        //[Key]
        //[Column(Order = 0)]
        [StringLength(5)]
        public string StateCode { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(5)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTimestamp { get; set; }
    }//end class
}//end namespace