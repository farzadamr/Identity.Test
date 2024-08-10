using System.ComponentModel.DataAnnotations;

namespace Identity.Test.Models.Dto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remmember Me")]
        public bool isPersistent { get; set; } = false;
        
    }
}
