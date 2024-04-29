using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Features.Medicos.Commands.RemoverMedico;
using AloDoutor.Application.Interfaces;
using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Commands.RemoverPaciente
{
    public class RemoverPacienteCommandHandler : IRequestHandler<RemoverPacienteCommand, Guid>
    {
        private readonly IPacienteRepository _pacienteRepository;
        public RemoverPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Guid> Handle(RemoverPacienteCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new RemoverPacienteCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            if (!_pacienteRepository.Buscar(p => p.Id == request.IdPaciente).Result.Any())
            {
                throw new BadRequestException("Medico Não localizado!", validationResult);
            }

            await _pacienteRepository.Remover(await _pacienteRepository.ObterPorId(request.IdPaciente));

            await _pacienteRepository.UnitOfWork.Commit();

            return request.IdPaciente;
        }
    }
}
