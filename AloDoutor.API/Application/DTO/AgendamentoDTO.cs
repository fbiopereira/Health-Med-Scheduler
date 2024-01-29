using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Api.Application.DTO
{
    public class AgendamentoDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PacienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid EspecialidadeMedicoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataHoraAtendimento { get; set; }


    }
}
