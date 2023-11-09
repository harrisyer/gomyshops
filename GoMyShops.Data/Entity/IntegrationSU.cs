using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("IntegrationSU")]
    public partial class IntegrationSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_IntegrationSU_IntegrationCode", 1, IsUnique = true)]
        public string IntegrationCode { get; set; }

        [Required]
        [StringLength(20)]
        //[Index("IX_IntegrationSU_IntegrationCode", 2, IsUnique = true)]
        public string IntegrationType { get; set; }

        [Required]
        [StringLength(30)]       
        public string IntegrationName { get; set; }

        [Required(AllowEmptyStrings =true)]
        [StringLength(250)]       
        public string URL { get; set; }
  
        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public int BusinessModel { get; set; }

    }//end class
}//end namespace