using MediatR;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public record ObterMedicosQuery : IRequest<IEnumerable<MedicoDTO>>;
   
}
