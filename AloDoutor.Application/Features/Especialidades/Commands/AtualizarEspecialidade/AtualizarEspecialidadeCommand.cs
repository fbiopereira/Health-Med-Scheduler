using MediatR;

namespace AloDoutor.Application.Features.Especialidades.Commands.AtualizarEspecialidade
{
    public class AtualizarEspecialidadeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

    }
}
