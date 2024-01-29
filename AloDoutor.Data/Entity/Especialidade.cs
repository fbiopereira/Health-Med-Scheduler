namespace AloDoutor.Domain.Entity
{
    public class Especialidade: Entidade
    {   
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }

        /* EF Relations */
        public IEnumerable<EspecialidadeMedico>? EspecialidadeMedicos { get; private set; }

        public Especialidade() { }
        public Especialidade(string nome, string? descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
