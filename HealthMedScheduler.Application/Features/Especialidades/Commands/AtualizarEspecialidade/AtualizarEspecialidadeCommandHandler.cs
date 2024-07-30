using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace HealthMedScheduler.Application.Features.Especialidades.Commands.AtualizarEspecialidade
{
    public class AtualizarEspecialidadeCommandHandler : IRequestHandler<AtualizarEspecialidadeCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        public AtualizarEspecialidadeCommandHandler(IMapper mapper, IEspecialidadeRepository especialidadeRepository)
        {
            _mapper = mapper;
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<Guid> Handle(AtualizarEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AtualizarEspecialidadeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Especialidade inválida", validationResult);

            var especialidadeUpdate = await _especialidadeRepository.ObterPorId(request.Id);

            //Validar se a especialidade está cadastrado na base
            if (especialidadeUpdate == null)
            {
                throw new BadRequestException("Especialidade Não localizada!", validationResult);
            }

            //Validar se a especialidade está cadastrado na base
            if (_especialidadeRepository.Buscar(p => p.Nome.ToLower() == request.Nome.ToLower() && p.Id != request.Id).Result.Any())
            {
                throw new BadRequestException("Falha ao atualizar especialidade!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var especialidadeAtualizado = _mapper.Map<Especialidade>(request);

            await _especialidadeRepository.Atualizar(especialidadeAtualizado);
            await _especialidadeRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return especialidadeAtualizado.Id;
        }
    }
}
