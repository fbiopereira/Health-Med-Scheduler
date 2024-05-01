using AloDoutor.Application.Interfaces;
using AloDoutor.Application.ViewModel;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Queries
{
    public class ObterEspecialidadesMedicoQueryHandler : IRequestHandler<ObterEspecialidadeMedicoQuery, IEnumerable<EspecialidadeMedicosViewModel>>,
     IRequestHandler<ObterMedicoEspecialidadePorIdQuery, EspecialidadeMedicosViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly ILogger<ObterEspecialidadesMedicoQueryHandler> _logger;

        public ObterEspecialidadesMedicoQueryHandler(IMapper mapper, IEspecialidadeMedicoRepository especiaidadeRepository, ILogger<ObterEspecialidadesMedicoQueryHandler> logger)
        {
            _mapper = mapper;
            _especialidadeMedicoRepository = especiaidadeRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<EspecialidadeMedicosViewModel>> Handle(ObterEspecialidadeMedicoQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _especialidadeMedicoRepository.ObterTodos();

            //Converte os objetos Pacientes para PacientesViewModel
            var data = _mapper.Map<IEnumerable<EspecialidadeMedicosViewModel>>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Lista de especialidades médicos foi retornada com sucesso");
            return data;
        }

        public async Task<EspecialidadeMedicosViewModel> Handle(ObterMedicoEspecialidadePorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var pacientes = await _especialidadeMedicoRepository.ObterPorId(request.idPaciente);

            //Converte os objetos pacientes para PacientesViewModel
            var data = _mapper.Map<EspecialidadeMedicosViewModel>(pacientes);

            //Retorna a lista de PacientesViewModel
            _logger.LogInformation("Especialdiade medico foi retornado com sucesso!");
            return data;
        }
    }
}
