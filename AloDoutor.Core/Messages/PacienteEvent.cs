using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages
{
    public class PacienteEvent
    {
        public PacienteEvent(Guid IdPaciente)
        {
            IdPaciente = IdPaciente;
        }

        public Guid IdPaciente { get; private set; }
    }
}
