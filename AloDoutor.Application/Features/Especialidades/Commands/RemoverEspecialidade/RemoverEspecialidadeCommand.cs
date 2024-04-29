using MediatR;

namespace AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade
{
    public class RemoverEspecialidadeCommand : IRequest<Guid>
    {
        public Guid IdEspecialdiade { get; set; }

    }
}
