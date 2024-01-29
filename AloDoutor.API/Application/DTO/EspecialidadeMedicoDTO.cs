using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Api.Application.DTO
{
    public class EspecialidadeMedicoDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid EspecialidadeId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid MedicoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataRegistro { get; set; }
    }
}
