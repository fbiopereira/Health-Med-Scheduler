using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Interfaces;
using MediatR;

namespace HealthMedScheduler.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico
{
    public class RemoverEspecialidadeMedicoCommandHandler : IRequestHandler<RemoverEspecialidadeMedicoCommand, Guid>
    {
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        public RemoverEspecialidadeMedicoCommandHandler(IEspecialidadeMedicoRepository especialdiadeMedicoRepository)
        {
            _especialidadeMedicoRepository = especialdiadeMedicoRepository;
        }

        public async Task<Guid> Handle(RemoverEspecialidadeMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new RemoverEspecialidadeMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            //Validar se a especialidade-Medico está cadastrado na base
            var especialidadeMedico = await _especialidadeMedicoRepository.ObterPorId(request.IdEspecialdiadeMedico);
            if (especialidadeMedico == null)
            {
                throw new BadRequestException("Especialidade-medico Não localizada!", validationResult); ;
            }

            await _especialidadeMedicoRepository.Remover(await _especialidadeMedicoRepository.ObterPorId(request.IdEspecialdiadeMedico));

            await _especialidadeMedicoRepository.UnitOfWork.Commit();

            return request.IdEspecialdiadeMedico;
        }
    }
}
