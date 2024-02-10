namespace AloFinances.Domain.Entity
{
    public class Paciente: Pessoa
    {
        public Paciente() { }
        public Paciente(string nome, string cpf, string cep, string endereco, string estado, string telefone, DateTime dataCriacao, bool ativo)
           : base(nome, cpf, cep, endereco, estado, telefone, dataCriacao, ativo)
        {
            
        }

        public void AtualizarPaciente(Paciente paciente)
        {            
            AtualizarPessoa(paciente);
        }

        public void InativarPaciente(bool ativo)
        {
            InativarPessoa(ativo);
        }
       
    }
}
