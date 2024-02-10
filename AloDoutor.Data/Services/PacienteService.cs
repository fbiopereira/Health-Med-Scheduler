using AloDoutor.Core.Messages;
using AloDoutor.Core.Messages.Integration;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Domain.Services
{
    public class PacienteService : CommandHandler, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly MassTransit.IBus _bus;
        private readonly ILogger _logger;

        public PacienteService(IPacienteRepository pacienteRepository, MassTransit.IBus bus, ILogger<PacienteService> logger)
        {
            _pacienteRepository = pacienteRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<ValidationResult> Adicionar(Paciente paciente)
        {
            //Validar se já existe um paciente cadastrado com esse cpf
            if (_pacienteRepository.Buscar(p => p.Cpf == paciente.Cpf).Result.Any())
            {
                AdicionarErro("Falha ao adicionar Paciente!");
                return ValidationResult;
            }          
            
            await _pacienteRepository.Adicionar(paciente);

            var sucesso = await PersistirDados(_pacienteRepository.UnitOfWork);
            if (sucesso.IsValid) await _bus.Publish(new PacienteEvent(paciente.Nome, paciente.Cpf, paciente.Cep, paciente.Endereco, paciente.Estado, paciente.Telefone, true));

            _logger.LogInformation("Mensagem publicada {classe} Data: {data}, Paciente: {paciente}", this, DateTime.Now, paciente);

            return sucesso;
        }

        public async Task<ValidationResult> Atualizar(Paciente paciente)
        {

            //Validar se o paciente está cadastrado na base
            if (!_pacienteRepository.Buscar(p => p.Id == paciente.Id).Result.Any())
            {
                AdicionarErro("Paciente Não localizado!");
                return ValidationResult;
            }

            ////Validar se existe algum outro cpf com esse mesmo numero vinculado a algum outro paciente
            if (_pacienteRepository.Buscar(p => p.Cpf == paciente.Cpf && p.Id != paciente.Id).Result.Any())            
            {
                AdicionarErro("Falha ao atualizar cliente!");
                return ValidationResult;
            }

            await _pacienteRepository.Atualizar(paciente);

            var sucesso = await PersistirDados(_pacienteRepository.UnitOfWork);
            if (sucesso.IsValid) 
                await _bus.Publish(new PacienteEvent(paciente.Nome, paciente.Cpf, paciente.Cep, paciente.Endereco, paciente.Estado, paciente.Telefone, true));

            _logger.LogInformation("Mensagem publicada: Atualizar Paciente {classe} Data: {data}, Paciente: {paciente}", this, DateTime.Now, paciente);

            return sucesso;
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            if (!_pacienteRepository.Buscar(p => p.Id == id).Result.Any())
            {
                AdicionarErro("Paciente Não localizado!");
                return ValidationResult;
            }
            var paciente = await _pacienteRepository.ObterPorId(id);

            await _pacienteRepository.Remover(paciente);

            var sucesso = await PersistirDados(_pacienteRepository.UnitOfWork);

            if (sucesso.IsValid)
                await _bus.Publish(new PacienteRemovidoEvent(paciente.Cpf));

            _logger.LogInformation("Mensagem publicada: Remover Paciente {classe} Data: {data}, Paciente: {paciente}", this, DateTime.Now, paciente);

            return sucesso;
        }
    }
}
