using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace MvcWebSchool_Identity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
