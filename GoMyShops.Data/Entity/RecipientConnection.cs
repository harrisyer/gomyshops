using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("RecipientConnection")]
    public partial class RecipientConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_RecipientConnection", 1, IsUnique = true)]
        public string RecipientConnectionCode { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientConnectionName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500)]
        public string Remarks { get; set; }

        [Required]
        [StringLength(30)]
        public string ConnectionType { get; set; }
        
        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }//end class
}//end namespace
