using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("RecipientGroup")]
    public partial class RecipientGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_RecipientGroupCode", 1, IsUnique = true)]
        public string RecipientGroupCode { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientGroupName { get; set; }

        [Required]
        [StringLength(2)]
        public string CompanyCode { get; set; }

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