using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Interfaces;
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
            var especialidades = await _especialidadeRepository.ObterTodos();

            //Converte os objetos Especialdiades para EspecialidadeViewModel
            var data = _mapper.Map<IEnumerable<EspecialidadeViewModel>>(especialidades);

            //Retorna a lista de EspecialidadeViewModel
            _logger.LogInformation("Lista de especialidades foi retornada com sucesso");
            return data;
        }

        public async Task<EspecialidadeViewModel> Handle(ObterEspecialidadePorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var especialidade = await _especialidadeRepository.ObterPorId(request.idEspecialidade);

            //Converte os objetos especialidade para EspecialidadeViewModel
            var data = _mapper.Map<EspecialidadeViewModel>(especialidade);

            //Retorna a lista de EspecialidadeViewModel
            _logger.LogInformation("Especialidade foi retornado com sucesso!");
            return data;
        }

        public async Task<EspecialidadeViewModel> Handle(ObterEspecialidadeMedicosPorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var especialidade = await _especialidadeRepository.ObterMedicosPorEspecialidadeId(request.idEspecialidade);

            //Converte os objetos especialidade para EspecialidadeViewModel
            var data = _mapper.Map<EspecialidadeViewModel>(especialidade);

            //Retorna a lista de EspecialidadeViewModel
            _logger.LogInformation("Especialidade foi retornado com sucesso!");
            return data;
        }
    }
}
