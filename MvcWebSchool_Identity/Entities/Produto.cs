using System.ComponentModel.DataAnnotations;

namespace MvcWebSchool_Identity.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres")]
        public string? Nome { get; set; }

        public decimal Preco { get; set; }
    }
}
