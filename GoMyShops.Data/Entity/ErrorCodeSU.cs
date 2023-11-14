using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("ErrorCodeSU")]
    public partial class ErrorCodeSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //[Index("IX_ErrorCodeSUErrorCode", 1, IsUnique = true)]
        [StringLength(10)]
        public string ErrorCode { get; set; }

        [StringLength(250)]
        [Required(AllowEmptyStrings = true)]
        public string ErrorMessage { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string ModelName { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = true)]
        public string FieldName { get; set; }

        [StringLength(250)]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
    }//end class
}//end namespace