using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class PacienteRemovidoEvent
    {
        public PacienteRemovidoEvent(string cpfPaciente)
        {
           CpfPaciente = cpfPaciente;
        }

        public string CpfPaciente { get; private set; }       
        
    }
}
