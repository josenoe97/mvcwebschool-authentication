using System.ComponentModel.DataAnnotations;

namespace MvcWebSchool_Identity.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senhar")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        public string? ConfirmPassword { get; set; }
    }
}
