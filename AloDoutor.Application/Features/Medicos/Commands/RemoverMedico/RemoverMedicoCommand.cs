using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AloDoutor.Application.Features.Medicos.Commands.RemoverMedico
{
    public class RemoverMedicoCommand : IRequest<Guid>
    {
        public Guid IdMedico { get; set; }

    }
}
