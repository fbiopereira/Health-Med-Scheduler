using MediatR;

namespace AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade
{
    public class AdicionarEspecialidadeCommand : IRequest<Guid>
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
