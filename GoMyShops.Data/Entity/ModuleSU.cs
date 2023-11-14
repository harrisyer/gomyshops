using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoMyShops.Data.Entity
{
    [Table("ModuleSU")]
    public partial class ModuleSU
    {
        //[Key]
        //[Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleID { get; set; }

        public int? ParentModuleID { get; set; }

        [StringLength(12)]
        public string? ResourceKey { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(12)]
        public string ModuleCode { get; set; } = string.Empty;

        [StringLength(30)]
        public string? ModuleName { get; set; }

        [StringLength(100)]
        public string? ModuleDesc { get; set; }

        public int ModuleSequence { get; set; }

        public int ModuleStatus { get; set; }

        [StringLength(1)]
        public string? Type { get; set; }

        [StringLength(1)]
        public string? Default { get; set; }

        [StringLength(30)]
        public string? TargetAction { get; set; }

        [StringLength(30)]
        public string? TargetController { get; set; }

        [StringLength(1)]
        public string? ApplicationType { get; set; }

        [StringLength(50)]
        public string? IconName { get; set; }

    }//end class
}//end namespace
