using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Document")]
    public partial class Documents
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        //[Index("IX_Document", 1, IsUnique = true)]
        public string DocumentCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500)]
        public string Descriptions { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(150)]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(250)]
        public string FilePath { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }
    }//end class
}//end namespace
