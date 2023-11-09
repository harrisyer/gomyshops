using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Bank")]
    public partial class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //[Index("IX_Bank_BankCode", 1, IsUnique = true)]
        [Required]
        [StringLength(20)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(1)]
        public string Status { get; set; }
    }//end class
}//end namespace
