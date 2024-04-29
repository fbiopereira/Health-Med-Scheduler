using AloDoutor.Application.Exceptions;
using AloDoutor.Application.Interfaces;
using AloDoutor.Domain.Entity;
using AutoMapper;
using MediatR;

namespace AloDoutor.Application.Features.Pacientes.Commands.AtualizarPaciente
{
    public class AtualizarPacienteCommandHandler : IRequestHandler<AtualizarPacienteCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;
        public AtualizarPacienteCommandHandler(IMapper mapper, IPacienteRepository pacienteRepository)
        {
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Guid> Handle(AtualizarPacienteCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AtualizarPacienteCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Paciente inválido", validationResult);

            if (!_pacienteRepository.Buscar(p => p.Id == request.Id).Result.Any())
            {
                throw new BadRequestException("Paciente Não localizado!", validationResult);

            }

            //Validar se existe algum cpf ou crm com esse mesmo numero vinculado a algum outro paciente
            if (_pacienteRepository.Buscar(p => p.Cpf == request.Cpf && p.Id != request.Id).Result.Any())
            {
                throw new BadRequestException("Falha ao atualizar Paciente!", validationResult);
            }

            //Converter para objeto entidade no dominio
            var pacienteAtualizado = _mapper.Map<Paciente>(request);

            await _pacienteRepository.Atualizar(pacienteAtualizado);
            await _pacienteRepository.UnitOfWork.Commit();
            // retorna o Guid Gerado
            return pacienteAtualizado.Id;
        }
    }
}
