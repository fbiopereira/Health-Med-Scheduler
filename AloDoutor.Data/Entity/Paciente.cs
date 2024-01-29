using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Domain.Entity
{
    public class Paciente: Pessoa
    {
        public string Idade { get; private set; }
        public IEnumerable<Agendamento>? Agendamentos { get; private set; }
    }
}
