using AloDoutor.Core.Messages;
using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AloDoutor.Domain.Services
{
    public class EspecialidadeService : CommandHandler, IEspecialidadeService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly ILogger _logger;

        public EspecialidadeService(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<ValidationResult> Adicionar(Especialidade especialidade)
        {
            //Validar se já existe uma especialidade cadastrada com esse cpf
            if (_especialidadeRepository.Buscar(p => p.Nome.ToLower() == especialidade.Nome.ToLower()).Result.Any())
            {
                AdicionarErro("Falha ao atualizar medico!");
                return ValidationResult;
            }

            await _especialidadeRepository.Adicionar(especialidade);

            return await PersistirDados(_especialidadeRepository.UnitOfWork);
            
        }

        public async Task<ValidationResult> Atualizar(Especialidade especialidade)
        {
            //Validar se a especialidade está cadastrado na base
            if (!_especialidadeRepository.Buscar(p => p.Id == especialidade.Id).Result.Any())
            {
                AdicionarErro("Especialidade Não localizada!");
                return ValidationResult;
            }
            
            await _especialidadeRepository.Atualizar(especialidade);

            return await PersistirDados(_especialidadeRepository.UnitOfWork);
        }

        public async Task<Especialidade> ObterPorId(Guid id)
        {
            var retorno = await _especialidadeRepository.ObterPorId(id);

            if (retorno != null)
                _logger.LogInformation("Obtem especialidade por ID na Service.");

            return retorno;
        }

        public async Task<List<Especialidade>> ObterTodos()
        {
            _logger.LogInformation("Obtendo todas as especialidades na Service.");
            var especialidade = await _especialidadeRepository.ObterTodos();
            return especialidade;
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            var especialidadeCadastrada = await _especialidadeRepository.ObterPorId(id);

            if (especialidadeCadastrada == null)
            {
                AdicionarErro("Especialidade não localizada!");
                return ValidationResult;
            }

            await _especialidadeRepository.Remover(especialidadeCadastrada);

            return await PersistirDados(_especialidadeRepository.UnitOfWork);
        }
        public async Task<Especialidade> ObterMedicosPorEspecialidadeId(Guid idEspecialidade)
        {
            var retorno = await _especialidadeRepository.ObterMedicosPorEspecialidadeId(idEspecialidade);

            if (retorno != null)
                _logger.LogInformation("Obtem medico por especialidade ID na Service.");

            return retorno;
        }
    }
}
