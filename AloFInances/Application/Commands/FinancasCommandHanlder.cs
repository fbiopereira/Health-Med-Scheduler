using AloDoutor.Core.Messages;
using FluentValidation.Results;
using MassTransit.Mediator;
using MediatR;

namespace AloFinances.Api.Application.Commands
{
    public class FinancasCommandHanlder : CommandHandler,
        IRequestHandler<PacienteComand, bool>
    {

        private readonly ILogger _logger;

        public FinancasCommandHanlder(ILogger<FinancasCommandHanlder> logger)
        {
            _logger = logger;
        }

        public async Task<bool> Handle(PacienteComand message, CancellationToken cancellationToken)
        {
            if (ValidarComando(message)) return false;

            Task.CompletedTask.Wait();

            return true;
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _logger.LogInformation($"Mensagem: {message}. Erro: {error}");
            }

            return false;
        }
    }
}
