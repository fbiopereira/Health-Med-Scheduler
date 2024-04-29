using AloDoutor.Application.Features.EspecialidadesMedicos.Queries;
using AloDoutor.Application.Interfaces;
using AloDoutor.Application.ViewModel;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Application.Features.Especialidades.Queries
{
    public class ObterEspecialidadeQueryHandler : IRequestHandler<ObterEspecialidadeQuery, IEnumerable<EspecialidadeViewModel>>,
     IRequestHandler<ObterEspecialidadePorIdQuery, EspecialidadeViewModel>,
     IRequestHandler<ObterEspecialidadeMedicosPorIdQuery, EspecialidadeViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly ILogger<ObterEspecialidadeQueryHandler> _logger;

        public ObterEspecialidadeQueryHandler(IMapper mapper, IEspecialidadeRepository especilialidadeRepository, ILogger<ObterEspecialidadeQueryHandler> logger)
        {
            _mapper = mapper;
            _especialidadeRepository = especilialidadeRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<EspecialidadeViewModel>> Handle(ObterEspecialidadeQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _especialidadeRepository.ObterTodos();

            //Converte os objetos Pacientes para EspecialidadeViewModel
            var data = _mapper.Map<IEnumerable<EspecialidadeViewModel>>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Lista de especialidades foi retornada com sucesso");
            return data;
        }

        public async Task<EspecialidadeViewModel> Handle(ObterEspecialidadePorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _especialidadeRepository.ObterPorId(request.idEspecialidade);

            //Converte os objetos pacientes para EspecialidadeViewModel
            var data = _mapper.Map<EspecialidadeViewModel>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Especialdiade foi retornado com sucesso!");
            return data;
        }

        public async Task<EspecialidadeViewModel> Handle(ObterEspecialidadeMedicosPorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _especialidadeRepository.ObterMedicosPorEspecialidadeId(request.idEspecialidade);

            //Converte os objetos pacientes para EspecialidadeViewModel
            var data = _mapper.Map<EspecialidadeViewModel>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Especialdiade foi retornado com sucesso!");
            return data;
        }
    }
}
