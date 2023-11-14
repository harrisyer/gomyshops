using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("EditDetail")]
    public partial class EditDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        //[Index("IX_EditDetail_DataModelKeyFieldValue", 1, IsUnique = false)]
        public string DataModel { get; set; }

        [Required]
        [StringLength(50)]
       // [Index("IX_EditDetail_DataModelKeyFieldValue", 2, IsUnique = false)]
        public string KeyFieldValue { get; set; }

        [Required(AllowEmptyStrings =true)]
        public string ParentValue { get; set; }
        
        [Required]
        public DateTime DateTimeStamp { get; set; }      

        [Required]
        public string CompositeKeys { get; set; }

        public string ValueChanges { get; set; }

        public string ValueBefore { get; set; }

        public string ValueAfter { get; set; }
    }//end class
}//end namespace
