using AloDoutor.Domain.Entity;
using AloDoutor.Domain.Interfaces;
using FluentValidation.Results;

namespace AloDoutor.Domain.Services
{
    public class MedicoService : CommandHandler, IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<ValidationResult> Adicionar(Medico medico)
        {           

            if (_medicoRepository.Buscar(p => p.Cpf == medico.Cpf || p.Crm == medico.Crm).Result.Any())
            {
                AdicionarErro("Falha ao adicionar medico!");
                return ValidationResult;
            }          

            await _medicoRepository.Adicionar(medico);

            return await PersistirDados(_medicoRepository.UnitOfWork);
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

            return await PersistirDados(_medicoRepository.UnitOfWork);


        }
     

        public async Task<ValidationResult> Remover(Guid id)
        {
            if (!_medicoRepository.Buscar(p => p.Id == id).Result.Any())
            {
                AdicionarErro("Medico Não localizado!");
                return ValidationResult;
            }
            await _medicoRepository.Remover(await _medicoRepository.ObterPorId(id));

            return await PersistirDados(_medicoRepository.UnitOfWork);
        }
    }
}
