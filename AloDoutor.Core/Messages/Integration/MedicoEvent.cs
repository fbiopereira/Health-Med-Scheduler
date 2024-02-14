using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class MedicoEvent
    {
        public MedicoEvent(string nome, string cpf, string cep, string endereco, string estado, string telefone, bool ativo, string crm)
        {
            Nome = nome;
            Cpf = cpf;
            Cep = cep;
            Endereco = endereco;
            Estado = estado;
            Telefone = telefone;
            Ativo = ativo;
            Crm = crm;
        }

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Cep { get; private set; }
        public string Endereco { get; private set; }
        public string Estado { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }
        public string Crm { get; private set; }
    }
}
