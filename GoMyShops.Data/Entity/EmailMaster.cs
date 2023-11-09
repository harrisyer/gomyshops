using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("EmailMaster")]
    public partial class EmailMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =true)]
        [StringLength(50)]
        public string EmailFrom { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(250)]
        public string EmailSubject { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string EmailTitle { get; set; }     
        
        public int TokenExpireHour { get; set; }

        [Required]
        [StringLength(250)]
        public string CompanyURL { get; set; }

        [Required]
        [StringLength(250)]
        public string CompanyWebSite { get; set; }

    }//end class
}//end namespace
