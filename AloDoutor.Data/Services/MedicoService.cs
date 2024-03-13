using AloDoutor.Core.Messages;
using AloDoutor.Core.Messages.Integration;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

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
            // Validação do CPF
            if (!ValidarCPF(medico.Cpf))
            {
                AdicionarErro("CPF inválido!");
                return ValidationResult;
            }

            // Validação do CRM
            if (!ValidarCRM(medico.Crm))
            {
                AdicionarErro("CRM inválido!");
                return ValidationResult;
            }

            if (_medicoRepository.Buscar(p => p.Cpf == medico.Cpf || p.Crm == medico.Crm).Result.Any())
            {
                AdicionarErro("Falha ao adicionar médico!");
                return ValidationResult;
            }

            await _medicoRepository.Adicionar(medico);

            var sucesso = await PersistirDados(_medicoRepository.UnitOfWork);
            if (sucesso.IsValid) await _bus.Publish(new MedicoEvent(medico.Nome, medico.Cpf, medico.Cep, medico.Endereco, medico.Estado, medico.Telefone, true, medico.Crm));

            _logger.LogInformation("Mensagem publicada: Cadastrar Médico {classe} Data: {data}, Médico: {medico}", this, DateTime.Now, medico);

            return sucesso;
        }

        public async Task<ValidationResult> Atualizar(Medico medico)
        {
            //Validar se o medico está cadastrado na base
            if (!_medicoRepository.Buscar(p => p.Id == medico.Id).Result.Any())
            {
                AdicionarErro("Medico Não localizado!");
                return ValidationResult;
            }

            //Validar se existe algum cpf ou crm com esse mesmo numero vinculado a algum outro medico
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

        public async Task<Medico> ObterPorId(Guid id)
        {
            var retorno = await _medicoRepository.ObterPorId(id);

            if (retorno != null)
                _logger.LogInformation("Obtem médico por ID na Service.");

            return retorno;
        }

        public async Task<IEnumerable<Medico>> ObterTodos()
        {
            _logger.LogInformation("Obtendo todos os médicos na Service.");
            var medicos = await _medicoRepository.ObterTodos();
            return medicos;
        }

        public async Task<Medico> ObterAgendamentosPorIdMedico(Guid idMedico)
        {
            var retorno = await _medicoRepository.ObterAgendamentosPorIdMedico(idMedico);
            if (retorno != null)
                _logger.LogInformation("Obtem agendamento por ID medico na Service.");
            return retorno;
        }

        public async Task<Medico> ObterEspecialidadesPorIdMedico(Guid idMedico)
        {
            var retorno = await _medicoRepository.ObterEspecialidadesPorIdMedico(idMedico);
            if (retorno != null)
                _logger.LogInformation("Obtem especialidade por ID medico na Service.");
            return retorno;
        }

        private bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool ValidarCRM(string crm)
        {
            // Regex para validar CRM médico no formato AA-12345
            Regex regex = new Regex(@"^[A-Za-z]{2}-\d{5}$");

            return regex.IsMatch(crm);
        }
    }
}
