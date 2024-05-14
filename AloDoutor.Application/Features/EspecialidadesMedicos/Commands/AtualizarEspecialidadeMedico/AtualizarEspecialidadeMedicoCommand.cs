using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico
{
    public class AtualizarEspecialidadeMedicoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid EspecialidadeId { get; set; }
        public Guid MedicoId { get; set; }
        public DateTime DataRegistro { get; set; }

    }
}
