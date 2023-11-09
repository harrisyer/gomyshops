using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("SysParameterSU")]
    public partial class SysParameterSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_SysParameterSUParamCode", 1, IsUnique = true)]
        public string ParamCode { get; set; }

        [Required]
        public string ParamValue { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }



    }//end class
}//end namespace