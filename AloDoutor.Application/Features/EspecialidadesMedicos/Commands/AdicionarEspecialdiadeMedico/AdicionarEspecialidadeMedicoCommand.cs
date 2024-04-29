using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialdiadeMedico
{
    public class AdicionarEspecialidadeMedicoCommand : IRequest<Guid>
    {
        public Guid EspecialidadeId { get; set; }
        public Guid MedicoId { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
