using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("DocSequenceSU")]
    public partial class DocSequenceSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //[Key]
        //[Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CompanyCode { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(20)]
        public string DistributorCode { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [StringLength(20)]
        public string BranchCode { get; set; }

        //[Key]
        //[Column(Order = 3)]
        [StringLength(20)]
        public string DocCode { get; set; }

        [StringLength(10)]
        public string Prefix { get; set; }

        public int? Length { get; set; }

        public int Sequence { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowsVersion { get; set; }
    }//end class
}//end namespace

