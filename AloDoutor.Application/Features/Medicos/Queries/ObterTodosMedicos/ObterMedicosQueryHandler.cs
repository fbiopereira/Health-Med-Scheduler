using AloDoutor.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterTodosMedicos
{
    public class ObterMedicosQueryHandler : IRequestHandler<ObterMedicosQuery, List<MedicoDTO>>
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
        public async Task<List<MedicoDTO>> Handle(ObterMedicosQuery request, CancellationToken cancellationToken)
        {
            //Obter dados do banco
            var medicos = await _medicoRepository.ObterTodos();

            //Converte os objetos médicos para MedicoDTO
            var data = _mapper.Map<List<MedicoDTO>>(medicos);

            //Retorna a lista de MedicoDTO
            _logger.LogInformation("Lista de médicos foi retornada com sucesso");
            return data;
        }
    }
}
