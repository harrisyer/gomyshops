using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("RecipientConnectionDetail")]
    public partial class RecipientConnectionDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string RecipientConnectionCode { get; set; }

        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        public string RecipientGroupCode { get; set; }

        [StringLength(20)]
        [Required(AllowEmptyStrings = true)]
        public string RecipientCode { get; set; }

        public int RecipientConnectionType { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }
    }//end class
}//end namespace
