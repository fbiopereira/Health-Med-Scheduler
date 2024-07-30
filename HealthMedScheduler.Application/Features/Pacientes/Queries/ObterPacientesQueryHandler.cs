using HealthMedScheduler.Application.ViewModel;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HealthMedScheduler.Application.Features.Pacientes.Queries
{
    public class ObterPacientesQueryHandler : IRequestHandler<ObterPacientesQuery, IEnumerable<PacienteViewModel>>,
     IRequestHandler<ObterPacientePorIdQuery, PacienteViewModel>,
     IRequestHandler<ObterAgendamentoPacientePorIdQuery, PacienteViewModel>
    // IRequestHandler<ObterAgendamentopacientePorIdpacienteQuery, pacienteViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ILogger<ObterPacientesQueryHandler> _logger;

        public ObterPacientesQueryHandler(IMapper mapper, IPacienteRepository pacienteRepository, ILogger<ObterPacientesQueryHandler> logger)
        {
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<PacienteViewModel>> Handle(ObterPacientesQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _pacienteRepository.ObterTodos();

            //Converte os objetos Pacientes para PacientesViewModel
            var data = _mapper.Map<IEnumerable<PacienteViewModel>>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Lista de médicos foi retornada com sucesso");
            return data;
        }

        public async Task<PacienteViewModel> Handle(ObterPacientePorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _pacienteRepository.ObterPorId(request.idPaciente);

            //Converte os objetos pacientes para PacientesViewModel
            var data = _mapper.Map<PacienteViewModel>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Paciente foi retornado com sucesso!");
            return data;
        }
        
        public async Task<PacienteViewModel> Handle(ObterAgendamentoPacientePorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var paciente = await _pacienteRepository.ObterAgendamentosPorIdPaciente(request.idPaciente);

            //Converte os objetos pacientes para PacienteViewModel
            var data = _mapper.Map<PacienteViewModel>(paciente);

            //Retorna pacienteViewModel
            _logger.LogInformation("paciente foi retornado com sucesso!");
            return data;
        }        
    }
}
