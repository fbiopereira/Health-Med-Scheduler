using AloDoutor.Domain.Entity.Common;

namespace AloDoutor.Domain.Entity
{
    public abstract class Pessoa : Entidade
    {
        public string? Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Cep { get; private set; }
        public string? Endereco { get; private set; }
        public string? Estado { get; private set; }
        public string? Telefone { get; private set; }

        protected Pessoa(string? nome, string? cpf, string? cep, string? endereco, string? estado, string? telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Telefone = telefone;
        }

        protected Pessoa()
        {
        }       
    }
}
