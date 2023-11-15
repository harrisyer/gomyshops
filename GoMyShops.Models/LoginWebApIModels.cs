using System.ComponentModel.DataAnnotations;

namespace GoMyShops.Models
{
    public class LoginWebApIModels
    {
        [Required]
        [MaxLength(255)]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
