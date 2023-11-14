using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("Email")]
    public partial class Email
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string EmailType { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(250)]
        public string EmailSubject { get; set; }

        [Required]
        public string EmailBody { get; set; }


    }//end class
}//end namespace
