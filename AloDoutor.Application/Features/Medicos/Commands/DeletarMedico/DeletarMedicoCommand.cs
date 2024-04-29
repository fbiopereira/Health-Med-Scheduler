using MediatR;

namespace AloDoutor.Application.Features.Medicos.Commands.DeletarMedico
{
    public class DeletarMedicoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
