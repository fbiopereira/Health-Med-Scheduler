using AloDoutor.Core.Messages;
using AloFinances.Domain.Entity;
using AloFinances.Domain.Interfaces;
using FluentValidation.Results;
using MassTransit.Initializers;
using MassTransit.Mediator;
using MediatR;

namespace AloFinances.Api.Application.Commands
{
    public class FinancasCommandHandler : CommandHandler,
        IRequestHandler<PacienteComand, ValidationResult>,
        IRequestHandler<PacienteRemovidoComand, ValidationResult>
    {

        private readonly ILogger _logger;
        private readonly IPacienteRepository _pacienteRepository;
        public FinancasCommandHandler(ILogger<FinancasCommandHandler> logger, IPacienteRepository pacienteRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ValidationResult> Handle(PacienteComand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            var paciente =  _pacienteRepository.Buscar(p => p.Cpf.Equals(message.Cpf)).Result.FirstOrDefault();

            if (paciente == null)
            {
                await _pacienteRepository.Adicionar(new Paciente(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.DataCadastro, message.Ativo));
                _logger.LogInformation("Data: {data}, Paciente Adicionado - CPF: {Cpf}, Nome: {Nome}", DateTime.Now, message.Cpf, message.Nome);
            }
            else
            {
                paciente.AtualizarPaciente(new Paciente(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.DataCadastro, message.Ativo));                
                await _pacienteRepository.Atualizar(paciente);
                _logger.LogInformation("Data: {data}, Paciente Atualizado - CPF: {Cpf}, Nome: {Nome}", DateTime.Now, message.Cpf, message.Nome);
            }   

            return await (PersistirDados(_pacienteRepository.UnitOfWork));
        }

        public async Task<ValidationResult> Handle(PacienteRemovidoComand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            var paciente = _pacienteRepository.Buscar(p => p.Cpf.Equals(message.Cpf)).Result.FirstOrDefault();

            if(paciente == null)
            {
                AdicionarErro("Paciente não localizado!");
                _logger.LogInformation("Data: {data}, Paciente Não localizado - CPF: {Cpf}", DateTime.Now, message.Cpf);
                return ValidationResult;
            }

            paciente.InativarPaciente(message.Ativo);

            await _pacienteRepository.Atualizar(paciente);

            _logger.LogInformation("Data: {data}, Paciente Inativado - CPF: {Cpf}", DateTime.Now, message.Cpf);

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
