using AloDoutor.Core.Messages;
using AloDoutor.Core.Messages.Integration;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Domain.Services
{
    public class MedicoService : CommandHandler, IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly MassTransit.IBus _bus;
        private readonly ILogger _logger;

        public MedicoService(IMedicoRepository medicoRepository, MassTransit.IBus bus, ILogger<MedicoService> logger)
        {
            _medicoRepository = medicoRepository;
            _bus = bus;
            _logger = logger;
        }

        public async Task<ValidationResult> Adicionar(Medico medico)
        {

            if (_medicoRepository.Buscar(p => p.Cpf == medico.Cpf || p.Crm == medico.Crm).Result.Any())
            {
                AdicionarErro("Falha ao adicionar medico!");
                return ValidationResult;
            }

            await _medicoRepository.Adicionar(medico);

            var sucesso = await PersistirDados(_medicoRepository.UnitOfWork);
            if (sucesso.IsValid) await _bus.Publish(new MedicoEvent(medico.Nome, medico.Cpf, medico.Cep, medico.Endereco, medico.Estado, medico.Telefone, true, medico.Crm));

            _logger.LogInformation("Mensagem publicada: Cadastrar Medico {classe} Data: {data}, Médico: {medico}", this, DateTime.Now, medico);

            return sucesso;
        }

        public async Task<ValidationResult> Atualizar(Medico medico)
        {

            //Validar se o paciente está cadastrado na base
            if (!_medicoRepository.Buscar(p => p.Id == medico.Id).Result.Any())
            {
                AdicionarErro("Medico Não localizado!");
                return ValidationResult;
            }


            //Validar se existe algum cpf ou crm com esse mesmo numero vinculado a algum outro paciente
            if (_medicoRepository.Buscar(p => (p.Cpf == medico.Cpf || p.Crm == medico.Cpf) && p.Id != medico.Id).Result.Any())
            {
                AdicionarErro("Falha ao atualizar Médico!");
                return ValidationResult;
            }

            await _medicoRepository.Atualizar(medico);

            var sucesso = await PersistirDados(_medicoRepository.UnitOfWork);
            if (sucesso.IsValid)
                await _bus.Publish(new MedicoEvent(medico.Nome, medico.Cpf, medico.Cep, medico.Endereco, medico.Estado, medico.Telefone, true, medico.Crm));

            _logger.LogInformation("Mensagem publicada: Atualizar Medico {classe} Data: {data}, Medico: {medico}", this, DateTime.Now, medico);

            return sucesso;


        }


        public async Task<ValidationResult> Remover(Guid id)
        {
            if (!_medicoRepository.Buscar(p => p.Id == id).Result.Any())
            {
                AdicionarErro("Medico Não localizado!");
                return ValidationResult;
            }
            var medico = await _medicoRepository.ObterPorId(id);

            await _medicoRepository.Remover(medico);


            var sucesso = await PersistirDados(_medicoRepository.UnitOfWork);

            if (sucesso.IsValid)
                await _bus.Publish(new MedicoRemovidoEvent(medico.Cpf, medico.Crm));

            _logger.LogInformation("Mensagem publicada: Remover Medico {classe} Data: {data}, Medico: {medico}", this, DateTime.Now, medico);

            return sucesso;
        }
    }
}
