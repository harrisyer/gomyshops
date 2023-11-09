using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("ParamSU")]
    public partial class ParamSU
    {
        [Key]
        [StringLength(15)]
        public string ParamCode { get; set; }

        [StringLength(50)]
        public string ParamCodeDesc { get; set; }

        public bool? IsSystem { get; set; }

        public virtual ICollection<Param> iParams { get; set; }

    }//end class
}//end namespace