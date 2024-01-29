using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;

namespace AloDoutor.Domain.Services
{
    public class EspecialidadeMedicoService : CommandHandler, IEspecialidadeMedicoService
    {
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IMedicoRepository _medicoRepository;

        public EspecialidadeMedicoService(IEspecialidadeMedicoRepository especialidadeMedicoRepository, IEspecialidadeRepository especialidadeRepository, IMedicoRepository medicoRepository)
        {
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
            _especialidadeRepository = especialidadeRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Adicionar(EspecialidadeMedico especialidadeMedico)
        {
            //Verificar se a especialidade está cadastrada na base de dados
            if (!_especialidadeRepository.Buscar(e => e.Id == especialidadeMedico.EspecialidadeId).Result.Any())
            {
                AdicionarErro("Especialidade não cadastrada na base de dados! ");
                return ValidationResult;
            }

            //Verificar se o medico está cadastrado na base de dados
            if (!_medicoRepository.Buscar(m => m.Id == especialidadeMedico.MedicoId).Result.Any())
            {
                AdicionarErro("Medico não cadastrado na base de dados! ");
                return ValidationResult;
            }


            //Verificar se já existe o medico já está vinculado a essa especialidade
            if(_especialidadeMedicoRepository.Buscar(e => e.EspecialidadeId == especialidadeMedico.EspecialidadeId && e.MedicoId == especialidadeMedico.MedicoId).Result.Any())
            {
                AdicionarErro("Esse médico já está vinculado a essa especialidade! ");
                return ValidationResult;
            }

            await _especialidadeMedicoRepository.Adicionar(especialidadeMedico);

            return await PersistirDados(_especialidadeMedicoRepository.UnitOfWork);
            
        }

        public async Task<ValidationResult> Atualizar(EspecialidadeMedico especialidadeMedico)
        {

            //Validar se a especialidade-Medico está cadastrado na base
            if (!_especialidadeMedicoRepository.Buscar(p => p.Id == especialidadeMedico.Id).Result.Any())
            {
                AdicionarErro("Especialidade-medico Não localizada!");
                return ValidationResult;
            }

            //Verificar se a especialidade está cadastrada na base de dados
            if (!_especialidadeRepository.Buscar(e => e.Id == especialidadeMedico.EspecialidadeId).Result.Any())
            {
                AdicionarErro("Especialidade não cadastrada na base de dados! ");
                return ValidationResult;
            }

            //Verificar se o medico está cadastrado na base de dados
            if (!_medicoRepository.Buscar(m => m.Id == especialidadeMedico.MedicoId).Result.Any())
            {
                AdicionarErro("Medico não cadastrado na base de dados! ");
                return ValidationResult;
            }


            //Validar se a essa idEspecialidade e idMedico já estão vinculados anteriormente
            if(_especialidadeMedicoRepository.Buscar(e => e.EspecialidadeId == especialidadeMedico.EspecialidadeId && e.MedicoId == especialidadeMedico.MedicoId && especialidadeMedico.Id != e.Id).Result.Any())
            {
                AdicionarErro("Esse vinculo entre medico e especialidade já existe!");
                return ValidationResult;
            }

            await _especialidadeMedicoRepository.Atualizar(especialidadeMedico);

            return await PersistirDados(_especialidadeMedicoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            if (!_especialidadeMedicoRepository.Buscar(e => e.Id == id).Result.Any())
            {
                AdicionarErro("Especialidade-Medico Não localizado!");
                return ValidationResult;
            }
            await _especialidadeMedicoRepository.Remover(await _especialidadeMedicoRepository.ObterPorId(id));

            return await PersistirDados(_especialidadeMedicoRepository.UnitOfWork);
        }
    }
}

