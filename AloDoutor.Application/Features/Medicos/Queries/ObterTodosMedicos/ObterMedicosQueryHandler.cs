using AloDoutor.Application.ViewModel;
using AloDoutor.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public class ObterMedicosQueryHandler : IRequestHandler<ObterMedicosQuery, IEnumerable<MedicoViewModel>>,
        IRequestHandler<ObterMedicoPorIdQuery, MedicoViewModel>,
        IRequestHandler<ObterEspecialidadePorIdMedicoQuery, MedicoViewModel>,
        IRequestHandler<ObterAgendamentoMedicoPorIdMedicoQuery, MedicoViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;
        private readonly ILogger<ObterMedicosQueryHandler> _logger;

        public ObterMedicosQueryHandler(IMapper mapper, IMedicoRepository medicoRepository, ILogger<ObterMedicosQueryHandler> logger)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<MedicoViewModel>> Handle(ObterMedicosQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var medicos = await _medicoRepository.ObterTodos();

            //Converte os objetos médicos para MedicoDTO
            var data = _mapper.Map<IEnumerable<MedicoViewModel>>(medicos);

            //Retorna a lista de MedicoDTO
            _logger.LogInformation("Lista de médicos foi retornada com sucesso");
            return data;
        }

        public async Task<MedicoViewModel> Handle(ObterMedicoPorIdQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var medicos = await _medicoRepository.ObterPorId(request.idMedico);

            //Converte os objetos médicos para MedicoDTO
            var data = _mapper.Map<MedicoViewModel>(medicos);

            //Retorna a lista de MedicoDTO
            _logger.LogInformation("Medico foi retornado com sucesso!");
            return data;
        }

        public async Task<MedicoViewModel> Handle(ObterEspecialidadePorIdMedicoQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var medico = await _medicoRepository.ObterEspecialidadesPorIdMedico(request.idMedico);

            //Converte os objetos médicos para MedicoViewModel
            var data = _mapper.Map<MedicoViewModel>(medico);

            //Retorna MedicoViewModel
            _logger.LogInformation("Medico foi retornado com sucesso!");
            return data;
        }

        public async Task<MedicoViewModel> Handle(ObterAgendamentoMedicoPorIdMedicoQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var medico = await _medicoRepository.ObterAgendamentosPorIdMedico(request.idMedico);

            //Converte os objetos médicos para MedicoViewModel
            var data = _mapper.Map<MedicoViewModel>(medico);

            //Retorna MedicoViewModel
            _logger.LogInformation("Medico foi retornado com sucesso!");
            return data;
        }
    }
}
