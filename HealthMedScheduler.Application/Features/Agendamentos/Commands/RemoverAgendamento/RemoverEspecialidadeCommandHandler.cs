using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Domain.Interfaces;
using MediatR;

namespace HealthMedScheduler.Application.Features.Agendamentos.Commands.RemoverAgendamento
{
    public class RemoverAgendamentoCommandHandler : IRequestHandler<RemoverAgendamentoCommand, Guid>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        public RemoverAgendamentoCommandHandler(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<Guid> Handle(RemoverAgendamentoCommand request, CancellationToken cancellationToken)
        {
            //Validar os dados inseridos
            var validator = new RemoverAgendamentoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Agendamento inválido", validationResult);

            var agendamento = await _agendamentoRepository.ObterPorId(request.IdAgendamento);

            if (agendamento == null)
            {
                throw new BadRequestException("Agendamento Não localizado!", validationResult);
            }

            await _agendamentoRepository.Remover(await _agendamentoRepository.ObterPorId(request.IdAgendamento));

            await _agendamentoRepository.UnitOfWork.Commit();

            return request.IdAgendamento;
        }
    }
}
