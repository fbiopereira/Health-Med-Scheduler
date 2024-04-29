namespace AloDoutor.Domain.Entity
{
    public class Medico : Pessoa
    {
        public string? Crm { get; private set; }       

        public IEnumerable<EspecialidadeMedico>? EspecialidadesMedicos { get; private set; }      

        public Medico(Guid id, string crm, IEnumerable<EspecialidadeMedico> especialidadesMedicos, string nome, string cpf, string cep, string endereco, string estado, string telefone)
       : base(id, nome, cpf, cep, endereco, estado, telefone) 
        {
            Crm = crm;
            EspecialidadesMedicos = especialidadesMedicos;
        }

        public Medico()
        {
        }
    }
}
