using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Param")]
    public partial class Param
    {
        //[Key]
        //[Column(Order = 0)]
        [StringLength(15)]
        public string ParamCode { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(20)]
        public string ParamValue { get; set; }

        [Required]
        [StringLength(100)]
        public string ParamDesc { get; set; }

        [Required]
        [StringLength(10)]
        public string ParamKey { get; set; }

        public int SortOrder { get; set; }

        public string ParamStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual ParamSU iParamSu { get; set; }

    }//end class
}//end namespace
