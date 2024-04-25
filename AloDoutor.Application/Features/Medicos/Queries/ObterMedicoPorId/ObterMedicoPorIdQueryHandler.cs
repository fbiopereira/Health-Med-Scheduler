using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.Medicos.Queries.ObterMedicoPorId
{
    public class ObterMedicoPorIdQueryHandler : IRequestHandler<ObterMedicoPorIdQuery, MedicoPorIdDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;

        public ObterMedicoPorIdQueryHandler(IMapper mapper, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
        }
        public async Task<MedicoPorIdDTO> Handle(ObterMedicoPorIdQuery request, CancellationToken cancellationToken)
        {
            // Consultar o banco de dados
            var medico = await _medicoRepository.ObterPorId(request.Id);

            // verificar se o registro existe
            if (medico == null)
                throw new NotFoundException(nameof(Medico), request.Id);

            // converter objeto de dados em objeto DTO
            var data = _mapper.Map<MedicoPorIdDTO>(medico);

            // retornar objeto DTO
            return data;
        }
    }
}
