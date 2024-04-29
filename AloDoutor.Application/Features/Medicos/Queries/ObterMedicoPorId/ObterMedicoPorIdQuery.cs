using MediatR;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterMedicoPorId
{
    public record ObterMedicoPorIdQuery(Guid Id) : IRequest<MedicoPorIdDTO>;
}
