using HealthMedScheduler.Domain.Entity.Common;

namespace HealthMedScheduler.Domain.Entity
{
    public abstract class Pessoa : Entidade
    {
        public string? Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Cep { get; private set; }
        public string? Endereco { get; private set; }
        public string? Estado { get; private set; }
        public string? Telefone { get; private set; }
        public string? Email { get; private set; }

        protected Pessoa(Guid id, string? nome, string? cpf, string? cep, string? endereco, string? estado, string? telefone, string? email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Telefone = telefone;
            Email = email;
        }

        protected Pessoa()
        {
        }       
    }
}
