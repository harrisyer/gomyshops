using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("ModuleActionSU")]
    public partial class ModuleActionSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string ModuleActionType { get; set; } 

        [StringLength(30)]
        public string ModuleName { get; set; } 
        //[Required]
        //[StringLength(30)]
        //public string ActionName { get; set; }

        public int ModuleID { get; set; }

        public bool MenuFlag { get; set; }

        public bool DetailFlag { get; set; }

        public bool CreateFlag { get; set; }

        public bool EditFlag { get; set; }

        public bool DeleteFlag { get; set; }

        public bool ApproveFlag { get; set; }

        [StringLength(1)]
        public string Status { get; set; } 

    }//end class
}//end namespace
