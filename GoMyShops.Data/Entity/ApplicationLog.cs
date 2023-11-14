using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace GoMyShops.Data.Entity
{
    //[Table("ApplicationLog")]
    public partial class ApplicationLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProcessEvent { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        public string Message { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsError { get; set; }
    }
}
