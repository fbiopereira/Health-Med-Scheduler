using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;

namespace AloDoutor.Domain.Services
{
    public class EspecialidadeService : CommandHandler, IEspecialidadeService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

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
    }
}
