using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico
{
    public class RemoverEspecialidadeMedicoCommand : IRequest<Guid>
    {
        public Guid IdEspecialdiadeMedico { get; set; }

    }
}
