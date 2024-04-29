using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Features.Especialidades.Commands.RemoverEspecialidade;
using AloDoutor.Application.Interfaces;
using MediatR;

namespace AloDoutor.Application.Features.EspecialidadesMedicos.Commands.RemoverEspecialidadeMedico
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

            if (!_especialidadeMedicoRepository.Buscar(p => p.Id == request.IdEspecialdiadeMedico).Result.Any())
            {
                throw new BadRequestException("Medico Não localizado!", validationResult);
            }

            await _especialidadeMedicoRepository.Remover(await _especialidadeMedicoRepository.ObterPorId(request.IdEspecialdiadeMedico));

            await _especialidadeMedicoRepository.UnitOfWork.Commit();

            return request.IdEspecialdiadeMedico;
        }
    }
}
