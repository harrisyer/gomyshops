using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Recipient")]
    public partial class Recipient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_Recipient", 1, IsUnique = true)]
        public string RecipientCode { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientFirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientLastName { get; set; }
               
        [StringLength(20)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(2)]
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