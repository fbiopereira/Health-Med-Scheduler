using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Api.Application.DTO
{
    public class EspecialidadeDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter mais que {2} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
