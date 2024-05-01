using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico
{
    public class AdicionarEspecialidadeMedicoCommandHandler : IRequestHandler<AdicionarEspecialidadeMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        public AdicionarEspecialidadeMedicoCommandHandler(IMapper mapper, IEspecialidadeMedicoRepository especialidadeMedicoRepository)
        {
            _mapper = mapper;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
        }
        public async Task<Guid> Handle(AdicionarEspecialidadeMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarEspecialidadeMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade Médico inválida", validationResult);

            //Converter para objeto entidade no dominio
            var especialidadeMedicoCriada = _mapper.Map<EspecialidadeMedico>(request);

            // Adicionar a base de dados
            await _especialidadeMedicoRepository.Adicionar(especialidadeMedicoCriada);
            await _especialidadeMedicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return especialidadeMedicoCriada.Id;
        }
    }
  
}
