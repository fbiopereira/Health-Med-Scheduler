namespace AloFinances.Domain.Entity
{
    public class Medico: Pessoa
    {
        public string Crm { get; private set; }

        public Medico() { }

        public Medico(string nome, string cpf, string cep, string endereco, string estado, string telefone, DateTime dataCriacao, bool ativo, string crm)
            : base(nome, cpf, cep, endereco, estado, telefone, dataCriacao, ativo)
        {
            Crm = crm;
        }

        public void AtualizarMedico(Medico medico)
        {
            AtualizarPessoa(medico);
            Crm = medico.Crm;
        }

        public void InativarMedico(bool ativo)
        {
            InativarPessoa(ativo);
        }
    }
}
