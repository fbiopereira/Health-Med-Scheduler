using AutoMapper;
using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Entity;
using MediatR;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarAgenda
{
    public class AdicionarAgendaMedicoCommandHandler : IRequestHandler<AdicionarAgendaMedicoCommand, Guid>
    {
        private readonly IMapper _mapper;

        public AdicionarAgendaMedicoCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AdicionarAgendaMedicoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new AdicionarAgendaMedicoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Médico inválido", validationResult);

            var agendaMedico =  _mapper.Map<AgendaMedico>(request);

            return Guid.NewGuid();
        }
    }
}
