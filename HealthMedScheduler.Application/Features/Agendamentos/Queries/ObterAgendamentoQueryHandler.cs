using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HealthMedScheduler.Application.Features.Agendamentos.Queries
{
    public class ObterAgendamentoQueryHandler : IRequestHandler<ObterAgendamentoQuery, IEnumerable<AgendamentoViewModel>>,
     IRequestHandler<ObterAgendamentoPorIdQuery, AgendamentoViewModel>,
     IRequestHandler<ObterAgendamentoPorStatusQuery, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly ILogger<ObterAgendamentoQueryHandler> _logger;

        public ObterAgendamentoQueryHandler(IMapper mapper, IAgendamentoRepository agendamentoRepository, ILogger<ObterAgendamentoQueryHandler> logger)
        {
            _mapper = mapper;
            _agendamentoRepository = agendamentoRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentoQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var agendamentos = await _agendamentoRepository.ObterTodosAgendamentos();

            //Converte os objetos Agendamentos para AgendamentoViewModel
            var data = _mapper.Map<IEnumerable<AgendamentoViewModel>>(agendamentos);

            //Retorna a lista de AgendamentoViewModel
            _logger.LogInformation("Lista de agendamentos foi retornada com sucesso");
            return data;
        }

        public async Task<AgendamentoViewModel> Handle(ObterAgendamentoPorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var agendamento = await _agendamentoRepository.ObterPorId(request.idAgendamento);

            //Converte os objetos agendamentos para AgendamentoViewModel
            var data = _mapper.Map<AgendamentoViewModel>(agendamento);

            //Retorna a lista de AgendamentoViewModel
            _logger.LogInformation("Agendamento foi retornado com sucesso!");
            return data;
        }

        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentoPorStatusQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorIStatus(request.status);

            //Converte os objetos agendamentos para AgendamentoViewModel
            var data = _mapper.Map<IEnumerable<AgendamentoViewModel>>(agendamentos);

            //Retorna a lista de AgendamentoViewModel
            _logger.LogInformation("Agendamento foi retornado com sucesso!");
            return data;
        }
    }
}
