using System.ComponentModel.DataAnnotations;

namespace FormProcessing.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string[] Hobbies { get; set; }
        [Required]
        [Range(1,40)]
        public int Roll { get; set; }
    }
}
