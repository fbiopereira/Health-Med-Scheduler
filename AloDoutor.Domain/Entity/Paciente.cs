namespace AloDoutor.Domain.Entity
{
    public class Paciente : Pessoa
    {
        public string? Idade { get; private set; }
        public IEnumerable<Agendamento>? Agendamentos { get; private set; }

       
    }
}
