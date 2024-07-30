using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente
{
    public class AdicionarPacienteCommandHandler : IRequestHandler<AdicionarPacienteCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;
        public AdicionarPacienteCommandHandler(IMapper mapper, IPacienteRepository pacienteRepository)
        {
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<Guid> Handle(AdicionarPacienteCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarPacienteCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Paciente inválido", validationResult);

            var pacienteCPF = await _pacienteRepository.ObterPacientePorCPF(request.Cpf);

            //Validar se já existe um paciente cadastrado com esse cpf
            if (pacienteCPF != null)
            {
                throw new BadRequestException("Falha ao adicionar Paciente!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var pacienteCriado = _mapper.Map<Paciente>(request);

            // Adicionar a base de dados
            await _pacienteRepository.Adicionar(pacienteCriado);
            await _pacienteRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return pacienteCriado.Id;
        }
    }
}
