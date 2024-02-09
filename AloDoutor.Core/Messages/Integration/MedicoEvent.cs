using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class MedicoEvent
    {
        public MedicoEvent(Guid IdMedico)
        {
            IdMedico = IdMedico;
        }

        public Guid IdMedico { get; private set; }
    }
}
