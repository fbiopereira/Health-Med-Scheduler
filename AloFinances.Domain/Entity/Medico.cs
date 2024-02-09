namespace AloFinances.Domain.Entity
{
    public class Medico: Pessoa
    {
        public string Crm { get; private set; }

        public Medico(string nome, string cpf, string cep, string endereco, string estado, string telefone, DateTime dataCriacao, bool ativo)
            : base(nome, cpf, cep, endereco, estado, telefone, dataCriacao, ativo)
        {
            // Outras inicializações específicas do Paciente, se necessário
        }
    }
}
