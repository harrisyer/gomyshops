using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("RiskScoreSU")]
    public partial class RiskScoreSU
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =true)]
        [StringLength(50)]
        public string ScoreName { get; set; }

        public bool IsDefault { get; set; }

        public int ScoreType { get; set; } // 1=, 2>=, 3<=,4=between include side

        public int StartScore { get; set; }

        public int EndScore { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }
    }//end class
}//end namespace
