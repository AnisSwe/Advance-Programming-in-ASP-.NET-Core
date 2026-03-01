using System.ComponentModel.DataAnnotations;

namespace FormProcessing.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please provide Username")]
        [StringLength(20,ErrorMessage = "Username shouldn't exceed 20")]
        public string UName { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 8)]
        public string Pass { get; set; }
    }
}
