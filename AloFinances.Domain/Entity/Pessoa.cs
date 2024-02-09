using AloDoutor.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloFinances.Domain.Entity
{
    public abstract class Pessoa : Entidade
    {

        protected Pessoa() { }
        protected Pessoa(string nome, string cpf, string cep, string endereco, string estado, string telefone, DateTime dataCriacao, bool ativo)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Telefone = telefone;
            DataCriacao = dataCriacao;
            Ativo = ativo;
        }

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Estado { get; private set; }
        public string Telefone { get; private set; }

        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
    }
}
