using AloDoutor.Core.Messages;
using AloFinances.Domain.Entity;
using AloFinances.Domain.Interfaces;
using FluentValidation.Results;
using MassTransit.Initializers;
using MassTransit.Mediator;
using MediatR;
using System.Drawing;

namespace AloFinances.Api.Application.Commands
{
    public class FinancasCommandHandler : CommandHandler,
        IRequestHandler<PacienteCommand, ValidationResult>,
        IRequestHandler<PacienteRemovidoComand, ValidationResult>,
        IRequestHandler<MedicoCommand, ValidationResult>,
        IRequestHandler<MedicoRemovidoComand, ValidationResult>,
        IRequestHandler<ContaCommand, ValidationResult>,
        IRequestHandler<ContaCanceladaComand, ValidationResult>

    {

        private readonly ILogger _logger;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPrecoRepository _precoRepository;
        private readonly IContaRepository _contaRepository;

        public FinancasCommandHandler(ILogger<FinancasCommandHandler> logger, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository, IPrecoRepository precoRepository, IContaRepository contaRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
            _precoRepository = precoRepository;
            _contaRepository = contaRepository;
        }

        public async Task<ValidationResult> Handle(PacienteCommand message, CancellationToken cancellationToken)
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

        public async Task<ValidationResult> Handle(MedicoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            var medico = _medicoRepository.Buscar(p => p.Cpf.Equals(message.Cpf)).Result.FirstOrDefault();

            if (medico == null)
            {
                await _medicoRepository.Adicionar(new Medico(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.DataCadastro, message.Ativo, message.Crm));
                _logger.LogInformation("Data: {data}, Medico Adicionado - CPF: {Cpf}, Nome: {Nome}", DateTime.Now, message.Cpf, message.Nome);
            }
            else
            {
                medico.AtualizarMedico(new Medico(message.Nome, message.Cpf, message.Cep, message.Endereco, message.Estado, message.Telefone, message.DataCadastro, message.Ativo, message.Crm));
                await _medicoRepository.Atualizar(medico);
                _logger.LogInformation("Data: {data}, Medico Atualizado - CPF: {Cpf}, Nome: {Nome}", DateTime.Now, message.Cpf, message.Nome);
            }

            return await (PersistirDados(_medicoRepository.UnitOfWork));
        }

        public async Task<ValidationResult> Handle(MedicoRemovidoComand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            var medico = _medicoRepository.Buscar(p => p.Cpf.Equals(message.Cpf) && p.Crm.Equals(message.Crm)).Result.FirstOrDefault();

            if (medico == null)
            {
                AdicionarErro("Medico não localizado!");
                _logger.LogInformation("Data: {data}, Medico Não localizado - CPF: {Cpf}", DateTime.Now, message.Cpf);
                return ValidationResult;
            }

            medico.InativarMedico(message.Ativo);

            await _medicoRepository.Atualizar(medico);

            _logger.LogInformation("Data: {data}, Medico Inativado - CPF: {Cpf}", DateTime.Now, message.Cpf);

            return await (PersistirDados(_medicoRepository.UnitOfWork));
        }

        public async Task<ValidationResult> Handle(ContaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;
            
            var paciente = _pacienteRepository.Buscar(p => p.Cpf.Equals(message.PacienteCPF)).Result.FirstOrDefault();

            if (paciente == null)
            {
                AdicionarErro("Paciente não localizado!");
                _logger.LogInformation("Data: {data}, Paciente Não localizado - CPF: {Cpf}", DateTime.Now, message.PacienteCPF);
                return ValidationResult;
            }

            var medico = _medicoRepository.Buscar(p => p.Crm.Equals(message.MedicoCrm)).Result.FirstOrDefault();

            if (medico == null)
            {
                AdicionarErro("Medico não localizado!");
                _logger.LogInformation("Data: {data}, Medico Não localizado - CRM: {crm}", DateTime.Now, message.MedicoCrm);
                return ValidationResult;
            }

            var conta = _contaRepository.Buscar( c => c.AgendamentoId == message.IdAgendamento).Result.FirstOrDefault();
            var preco = await _precoRepository.ObterTodos();
            if (conta == null)
            {                
                var novaConta = new Contas(paciente.Id, medico.Id, message.DataAgendamento, StatusConta.PENDENTE, message.IdAgendamento);
                novaConta.CalcularValor(medico.Valor, preco.First().Valor);
                await _contaRepository.Adicionar(novaConta);
                _logger.LogInformation("Data: {data}, Conta Atualizada - ID Agendamento: {idAgendamento}", DateTime.Now, message.IdAgendamento);
            }
            else
            {
                conta.AtualizarConta(paciente.Id, medico.Id, message.DataAgendamento);
                await _contaRepository.Atualizar(conta);
                _logger.LogInformation("Data: {data}, Conta Atualizada - Codigo Conta: {codigo}, ID Agendamento: {Nome}", DateTime.Now, conta.Codigo, conta.AgendamentoId);
            }

            return await (PersistirDados(_contaRepository.UnitOfWork));

        }

        public async Task<ValidationResult> Handle(ContaCanceladaComand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return ValidationResult;

            var conta = _contaRepository.Buscar( a => a.AgendamentoId == message.IdAgendamento).Result.FirstOrDefault();

            if (conta == null)
            {
                AdicionarErro("Conta não localizada");
                _logger.LogInformation("Data: {data}, Conta Não localizada - ID Agendamento: {idAgendamento}", DateTime.Now, message.IdAgendamento);
                return ValidationResult;
            }

            conta.CancelarConta();

            _logger.LogInformation("Data: {data}, Conta Cancelada - Codigo Conta: {codigo}, ID Agendamento: {agedamentoId}", DateTime.Now, conta.Codigo, conta.AgendamentoId);

            await _contaRepository.Atualizar(conta);

            return await (PersistirDados(_medicoRepository.UnitOfWork));
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
