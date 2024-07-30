using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.AtualizarEspecialidadeMedico
{
    public class AtualizarEspecialidadeMedicoCommandHandler : IRequestHandler<AtualizarEspecialidadeMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IMedicoRepository _medicoRepository;
        public AtualizarEspecialidadeMedicoCommandHandler(IMapper mapper, IEspecialidadeMedicoRepository especialidadeMedicoRepository, IEspecialidadeRepository especialidadeRepository, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _especialidadeRepository = especialidadeRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<Guid> Handle(AtualizarEspecialidadeMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AtualizarEspecialidadeMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade Médico inválido", validationResult);

            //Validar se a especialidade-Medico está cadastrado na base
            var especialidadeMedico = await _especialidadeMedicoRepository.ObterPorId(request.Id);
            if (especialidadeMedico == null)
            {
                throw new BadRequestException("Especialidade-medico Não localizada!", validationResult); ;
            }

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


            //Validar se a essa idEspecialidade e idMedico já estão vinculados anteriormente
            if (_especialidadeMedicoRepository.Buscar(e => e.EspecialidadeId == request.EspecialidadeId && e.MedicoId == request.MedicoId && request.Id != e.Id).Result.Any())
            {
                throw new BadRequestException("Esse vinculo entre medico e especialidade já existe!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var medicoAtualizado = _mapper.Map<EspecialidadeMedico>(request);

            await _especialidadeMedicoRepository.Atualizar(medicoAtualizado);
            await _especialidadeMedicoRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return medicoAtualizado.Id;
        }
    }
}
