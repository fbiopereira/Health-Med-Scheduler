using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AdicionarEspecialidadeMedico
{
    public class AdicionarEspecialidadeMedicoCommandHandler : IRequestHandler<AdicionarEspecialidadeMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IMedicoRepository _medicoRepository;
        public AdicionarEspecialidadeMedicoCommandHandler(IMapper mapper, IEspecialidadeMedicoRepository especialidadeMedicoRepository, IEspecialidadeRepository especialidadeRepository, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _especialidadeRepository = especialidadeRepository;
            _medicoRepository = medicoRepository;
        }
        public async Task<Guid> Handle(AdicionarEspecialidadeMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarEspecialidadeMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade Médico inválida", validationResult);

            //Verificar se a especialidade está cadastrada na base de dados
            var especialidade = await _especialidadeRepository.ObterPorId(request.EspecialidadeId);
            if (especialidade == null)
            {
                throw new BadRequestException("Especialidade não cadastrada na base de dados!", validationResult);
            }

            //Verificar se o medico está cadastrado na base de dados
            var medico = await _medicoRepository.ObterPorId(request.MedicoId);
            if (medico == null)
            {
                throw new BadRequestException("Medico não cadastrado na base de dados! ", validationResult);
            }

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
