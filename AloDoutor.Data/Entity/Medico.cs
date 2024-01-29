namespace AloDoutor.Domain.Entity
{
    public class Medico : Pessoa
    {
        public string Crm { get; private set; }

        public IEnumerable<EspecialidadeMedico>? EspecialidadesMedicos { get; private set; }

        public Medico() { }
    }
}
