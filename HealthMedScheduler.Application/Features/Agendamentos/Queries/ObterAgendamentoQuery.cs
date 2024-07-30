using HealthMedScheduler.Application.ViewModel;
using MediatR;

namespace HealthMedScheduler.Application.Features.Agendamentos.Queries
{
    public record ObterAgendamentoQuery : IRequest<IEnumerable<AgendamentoViewModel>>;
    public record ObterAgendamentoPorIdQuery(Guid idAgendamento) : IRequest<AgendamentoViewModel>;
    public record ObterAgendamentoPorStatusQuery(int status) : IRequest<IEnumerable<AgendamentoViewModel>>;

}
