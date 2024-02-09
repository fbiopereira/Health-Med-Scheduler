using AloDoutor.Core.Messages;
using AloFinances.Domain.Entity;
using AloFinances.Domain.Interfaces;
using FluentValidation.Results;
using MassTransit.Mediator;
using MediatR;

namespace AloFinances.Api.Application.Commands
{
    public class FinancasCommandHanlder : CommandHandler,
        IRequestHandler<PacienteComand, ValidationResult>
    {

        private readonly ILogger _logger;
        private readonly IPacienteRepository _pacienteRepository;
        public FinancasCommandHanlder(ILogger<FinancasCommandHanlder> logger, IPacienteRepository pacienteRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ValidationResult> Handle(PacienteComand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            await _pacienteRepository.Adicionar(new Paciente(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.DataCadastro, message.Ativo));

            _logger.LogInformation("Data: {data}, Paciente Adicionado - CPF: {Cpf}, Nome: {Nome}", DateTime.Now, message.Cpf, message.Nome);

            return await (PersistirDados(_pacienteRepository.UnitOfWork));
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
