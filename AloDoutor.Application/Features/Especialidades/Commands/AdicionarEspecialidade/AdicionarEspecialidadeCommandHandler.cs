using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Features.Medicos.Commands.AdicionarMedico;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.Especialidades.Commands.AdicionarEspecialidade
{
    public class AdicionarEspecialidadeCommandHandler : IRequestHandler<AdicionarEspecialidadeCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        public AdicionarEspecialidadeCommandHandler(IMapper mapper, IEspecialidadeRepository especialidadeRepository)
        {
            _mapper = mapper;
            _especialidadeRepository = especialidadeRepository;
        }
        public async Task<Guid> Handle(AdicionarEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarEspecialidadeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade inválida", validationResult);

            //Converter para objeto entidade no dominio
            var especialidadeCriada = _mapper.Map<Especialidade>(request);

            // Adicionar a base de dados
            await _especialidadeRepository.Adicionar(especialidadeCriada);
            await _especialidadeRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return especialidadeCriada.Id;
        }
    }
}
