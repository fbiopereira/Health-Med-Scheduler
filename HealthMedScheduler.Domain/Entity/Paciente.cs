namespace HealthMedScheduler.Domain.Entity
{
    public class Paciente : Pessoa
    {
        public Paciente(Guid id, string idade, IEnumerable<Agendamento> agendamentos, string nome, string cpf, string cep, string endereco, string estado, string telefone)
            : base(id, nome, cpf, cep, endereco, estado, telefone)
        {
            Idade = idade;
            Agendamentos = agendamentos;
        }      

        public Paciente() { }

        public string? Idade { get; private set; }
        public IEnumerable<Agendamento>? Agendamentos { get; private set; }

       
    }
}
