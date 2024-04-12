using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AloDoutor.Application.DTO
{
    public class PacienteDTO
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter mais que {2} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 11)]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter mais que {2} caracteres", MinimumLength = 2)]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter mais que {2} caracteres", MinimumLength = 2)]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Idade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Telefone { get; set; }


    }
}
